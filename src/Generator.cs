using Godot;
using System;

enum ModeColorChange
{
    CROP_LINES,
    CENTER_LINES,
    BACKGROUND,
    BACKGROUND_LINES,
    TEXT_ONE,
    LINE,
    TEXT_SECOND,
    TEXT_THREE
}
public class Generator : Control
{
    public Viewport viewport;
    public Image image;

    ModeColorChange mode;

    ColorRect back;
    ColorRect line;
    Label first;
    Label second;
    Label three;
    Control crop;
    Control centerline;
    Control backlines;
    Control backlines2;

    TabContainer tabContainer;
    ColorPicker coolorPicker;
    LineEdit ledit1;
    LineEdit ledit2;
    LineEdit ledit3;
    CheckBox checkBox;
    Button saveButton;
    FileDialog menu;

    public override void _Ready()
    {

        viewport = GetNode<Viewport>("view");
        back = viewport.GetNode<ColorRect>("background");
        line = viewport.GetNode<ColorRect>("line");
        first = viewport.GetNode<Label>("first");
        three = viewport.GetNode<Label>("three");
        second = viewport.GetNode<Label>("second");
        crop = viewport.GetNode<Control>("crop");
        centerline = viewport.GetNode<Control>("centerline");
        backlines = viewport.GetNode<Control>("backlines");
        backlines2 = viewport.GetNode<Control>("backlines2");

        tabContainer= GetNode<TabContainer>("TabContainer");
        coolorPicker = GetNode<ColorPicker>("ColorPicker");
        ledit1 = GetNode<LineEdit>("LineEdit");
        ledit2 = GetNode<LineEdit>("LineEdit2");
        ledit3 = GetNode<LineEdit>("LineEdit3");
        checkBox = GetNode<CheckBox>("CheckBox");
        saveButton = GetNode<Button>("Button");
        menu = GetNode<FileDialog>("PopupMenu");

        ledit1.Connect("text_changed",this,"ChangeName");
        ledit2.Connect("text_changed",this,"ChangeScale");
        ledit3.Connect("text_changed",this,"ChangeMark");
        checkBox.Connect("toggled",this,"IsEnableLine");
        saveButton.Connect("button_down",this,"ViewPopMenu");
        menu.Connect("file_selected",this,"MenuDirSelected");
        menu.Connect("confirmed",this,"MenuConfirmed");
        tabContainer.Connect("tab_changed",this,"ChangedTab");
        coolorPicker.Connect("color_changed",this,"ColorChanged");

    }
    string pathTosave;
    Color currentColor;
    public void ChangedTab(int index)
    {
        switch (index)
        {
            case 0: mode = ModeColorChange.CROP_LINES;
                break;
            case 1: mode = ModeColorChange.CENTER_LINES;
                break;
            case 2: mode = ModeColorChange.BACKGROUND;
                break;
            case 3: mode = ModeColorChange.BACKGROUND_LINES;
                break;
            case 4: mode = ModeColorChange.TEXT_ONE;
                break;
             case 5: mode = ModeColorChange.LINE;
                break;
            case 6: mode = ModeColorChange.TEXT_SECOND;
                break;
             case 7: mode = ModeColorChange.TEXT_THREE;
                break;
            default:break;
        }
    }
    public void ColorChanged(Color color)
    {
        switch (mode)
        {
            case ModeColorChange.CROP_LINES: crop.Modulate = color;
                break;
            case ModeColorChange.CENTER_LINES: centerline.Modulate = color;
                break;
            case ModeColorChange.BACKGROUND: back.Color = color;
                break;
            case ModeColorChange.BACKGROUND_LINES: backlines.Modulate = backlines2.Modulate = color;
                break;
            case ModeColorChange.TEXT_ONE: first.Modulate = color;
                break;
            case ModeColorChange.LINE: line.Color = color;
                break;
            case ModeColorChange.TEXT_SECOND: second.Modulate = color;
                break;
            case ModeColorChange.TEXT_THREE: three.Modulate = color;
                break;
            default:break;
        }
    }

    public void MenuConfirmed()
    {
        menu.Hide();
    }
    public void MenuDirSelected(string dir)
    {
        pathTosave = dir;
        GD.Print(dir);
        image = viewport.GetTexture().GetData();
        image.FlipY();
        //TODO:Change Resize
        image.SavePng(pathTosave);
    }

    public void ViewPopMenu()
    {
        menu.Show();
    }

    public void IsEnableLine(bool enable)
    {
        line.Visible = enable;
    }
    public void ChangeName(string name)
    {
        first.Text = name;
    }
    public void ChangeMark(string name)
    {
        three.Text = name;
    }
    public void ChangeScale(string scale)
    {
        second.Text = scale;
    }






    public void SaveViewport()
    {
        image = viewport.GetTexture().GetData();
        image.FlipY();
        image.SavePng(System.AppContext.BaseDirectory + "testing.png");
    }

}
