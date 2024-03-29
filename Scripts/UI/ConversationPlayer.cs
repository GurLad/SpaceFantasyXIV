using Godot;
using System;
using System.Collections.Generic;
using System.Linq;

public partial class ConversationPlayer : Control
{
    private enum State { Idle, Writing, WaitForInput }

    [Export]
    private Control container;
    [Export]
    private Godot.Collections.Array<TextureRect> portraits;
    [Export]
    private Label text;
    [Export]
    private float textSpeed;
    [Export]
    private float moveTime;
    [Export]
    private float showHideTime;
    private Interpolator interpolator = new Interpolator();
    private Timer timer = new Timer();
    private float shownHeight;
    private float hiddenHeight;
    private float leftX;
    private float rightX;
    private State state;
    private List<string> lines = new List<string>();
    private string currentLine;
    private int currentSpeaker;

    [Signal]
    public delegate void FinishedConversationEventHandler();

    public override void _Ready()
    {
        base._Ready();
        AddChild(interpolator);
        AddChild(timer);
        timer.WaitTime = 1 / textSpeed;
        timer.Timeout += NextLetter;
        leftX = container.Position.X;
        rightX = container.Position.X - (Size.X - 256);
        shownHeight = Position.Y;
        hiddenHeight = Position.Y + Size.Y;
        Position = new Vector2(Position.X, hiddenHeight);
    }

    public override void _Input(InputEvent @event)
    {
        if (@event is InputEventMouseButton eventMouse)
        {
            if (eventMouse.Pressed && eventMouse.ButtonIndex == MouseButton.Left)
            {
                if (state == State.Writing)
                {
                    text.Text += currentLine;
                    currentLine = "";
                    timer.Stop();
                    state = State.WaitForInput;
                    return;
                }
                else if (state == State.WaitForInput)
                {
                    NextLine();
                }
            }
        }
    }

    public void BeginConversation(string content)
    {
        text.Text = "";
        lines = content.Trim().Replace("\r", "").Split("\n").ToList();
        currentSpeaker = lines[0][0] == '1' ? 0 : 1;
        container.Position = new Vector2(currentSpeaker == 0 ? leftX : rightX, container.Position.Y);
        interpolator.Interpolate(showHideTime,
            new Interpolator.InterpolateObject(
                a => Position = new Vector2(Position.X, a),
                hiddenHeight,
                shownHeight,
                Easing.EaseOutQuad));
        interpolator.OnFinish = () => NextLine();
    }

    public void NextLine()
    {
        try
        {
            void TrueBegin()
            {
                text.Text = "";
                timer.Start();
                state = State.Writing;
                lines.RemoveAt(0);
            }

            state = State.Idle;
            if (lines.Count <= 0)
            {
                HideUI();
                return;
            }
            string[] parts = lines[0].Split(":");
            string[] portraitParts = parts[0].Split(",");
            int id = int.Parse(portraitParts[0]) - 1;
            portraits[id].Texture = PortraitController.Current.GetPortrait(id == 0 ? "MC" : (FormController.BossPhase > 4 ? "EnemyNkd" : "Enemy"), portraitParts[1]);
            currentLine = parts[1].Trim();
            if (id != currentSpeaker)
            {
                interpolator.Interpolate(moveTime,
                    new Interpolator.InterpolateObject(
                        a => container.Position = new Vector2(a, container.Position.Y),
                        container.Position.X,
                        id == 0 ? leftX : rightX,
                        Easing.EaseOutQuad));
                interpolator.OnFinish = TrueBegin;
                currentSpeaker = id;
            }
            else
            {
                TrueBegin();
            }
        }
        catch
        {
            if (lines.Count > 0)
            {
                GD.PrintErr("Error in line " + lines[0] + "!");
                lines.RemoveAt(0);
                NextLine();
            }
            else
            {
                GD.PrintErr("Error no lines!");
                HideUI();
            }
        }
    }

    private void NextLetter()
    {
        if (state != State.Writing)
        {
            return;
        }
        text.Text += currentLine[0];
        if (currentLine.Length <= 1)
        {
            timer.Stop();
            state = State.WaitForInput;
            return;
        }
        currentLine = currentLine.Substring(1);
    }

    private void HideUI()
    {
        state = State.Idle;
        interpolator.Interpolate(showHideTime,
            new Interpolator.InterpolateObject(
                a => Position = new Vector2(Position.X, a),
                shownHeight,
                hiddenHeight,
                Easing.EaseInQuad));
        interpolator.OnFinish = () =>
        {
            text.Text = "";
            EmitSignal(SignalName.FinishedConversation);
        };
    }
}
