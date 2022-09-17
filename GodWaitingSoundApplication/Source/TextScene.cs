using System;
using System.Drawing;
using System.Windows.Forms;

namespace GodWaitingSoundApplication
{
    class TextScene : SceneDrawer
    {
        Label label;

        public TextScene(MainEditor form, string text = "אין סאונד") : base(form)
        {
            label = Utilities.CreateLabel(text, 16);
            label.Location = new Point((MainEditor.EditorWidth - label.Width) / 2, form.SceneStartHeight);
            label.Visible = false;
            form.Controls.Add(label);
        }

        public override void Load()
        {
            base.Load();
            label.Visible = true;
        }

        public override void Unload()
        {
            label.Visible = false;
            base.Unload();
        }
    }
}