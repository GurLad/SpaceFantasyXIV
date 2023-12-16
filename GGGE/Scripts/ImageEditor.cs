using Godot;
using System;

public partial class ImageEditor : HBoxContainer
{
    // Exports
    [Export]
    private string dataKey;
    [Export]
    private AGameDataLoader loader;
    [Export]
    private Vector2I targetResolution;
    [ExportCategory("Internal exports")]
    [Export]
    private TextureRect previewRect;
    [Export]
    private Button browseButton;
    [Export]
    private FileDialog fileDialog;
    // Properties
    private Sprite2D _data;
    protected Sprite2D data => _data;

    [Signal]
    public delegate void OnDirtyEventHandler();

    public override void _Ready()
    {
        base._Ready();
        previewRect.CustomMinimumSize = targetResolution;
        _data = loader.GetSprite(dataKey);
        OnDirty += () => loader.EmitSignal(AGameDataLoader.SignalName.OnDirty);
        loader.OnExternalChange += () => previewRect.Texture = data.Texture;
        // Init file dialog
        if (fileDialog == null)
        {
            fileDialog = new FileDialog();
            InitFileDialog(fileDialog);
        }
        fileDialog.FileSelected += (path) =>
        {
            previewRect.Texture = data.Texture = ImageTexture.CreateFromImage(Image.LoadFromFile(path));
            SetDirty();
        };
        browseButton.Pressed += fileDialog.Show;
        AddChild(fileDialog);
    }

    protected void SetDirty() => EmitSignal(SignalName.OnDirty);

    private void InitFileDialog(FileDialog dialog)
    {
        fileDialog.Filters = new string[] { "*.png, *.gif, *.jpg ; Image files" };
        fileDialog.FileMode = FileDialog.FileModeEnum.OpenFile;
        fileDialog.Access = FileDialog.AccessEnum.Filesystem;
        //Window root = GetTree().Root;
        //fileDialog.ContentScaleAspect = root.ContentScaleAspect;
        //fileDialog.ContentScaleFactor = root.ContentScaleFactor;
        //fileDialog.ContentScaleMode = root.ContentScaleMode;
        //fileDialog.ContentScaleSize = root.ContentScaleSize;
        //fileDialog.ContentScaleStretch = root.ContentScaleStretch;
        fileDialog.InitialPosition = Window.WindowInitialPosition.CenterPrimaryScreen;
        //fileDialog.Mode = Window.ModeEnum.Maximized;
        fileDialog.Theme = previewRect.Theme;
        fileDialog.UseNativeDialog = true;
    }
}
