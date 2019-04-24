using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Drawing;
using System.ComponentModel;

namespace RMMV_PackMan
{
    public class AdvanceTextBox : TextBox, ISupportInitialize
    {
        private string placeholderText = "";
        [Browsable(true)]
        [EditorBrowsable(EditorBrowsableState.Always)]
        [Category("Placeholder Text")]
        [Description("Text to show when the Text property is empty.")]
        [DefaultValue("")]
        public string PlaceholderText
        {
            get
            {
                return placeholderText;
            }
            set
            {
                SetTextToPlaceholder(value);
                 placeholderText = value;
                Invalidate();
            }
        }

        /// <summary>
        /// Gets or sets the text associated with this control.
        /// </summary>
        public new string Text
        {
            get
            {
                if (base.Text != PlaceholderText)
                    return base.Text;
                return string.Empty;
            }
            set
            {
                base.Text = value;
                if (base.Text == PlaceholderText)
                    ForeColor = PlaceholderForeColor;
                else
                    ForeColor = defaultForeColour;
            }
        }

        private Color placeholderForecolour = Color.FromKnownColor(KnownColor.ControlDarkDark);

        [Browsable(true)]
        [EditorBrowsable(EditorBrowsableState.Always)]
        [Category("Placeholder Text")]
        [Description("The color of the placeholder text.")]
        public Color PlaceholderForeColor { get => placeholderForecolour; set => placeholderForecolour = value; }


        bool initializing;
        
        Color defaultForeColour;
        public AdvanceTextBox()
        {
            defaultForeColour = ForeColor;
            GotFocus += AdvanceTextBox_GotFocus;
            LostFocus += AdvanceTextBox_LostFocus;
        }

        private void AdvanceTextBox_LostFocus(object sender, EventArgs e)
        {
            if (ShouldShowPlaceHolderText())
                Text = PlaceholderText;
        }

        private void AdvanceTextBox_GotFocus(object sender, EventArgs e)
        {
            if (IsShowingPlaceholderText())               
                Text = string.Empty;

        }

       

        public void BeginInit()
        {
            initializing = true;
        }

        public void EndInit()
        {
            initializing = false;
            SetTextToPlaceholder(PlaceholderText);
        }

        void SetTextToPlaceholder(string val)
        {
            if (ShouldShowPlaceHolderText())
            {
                Text = val;
                ForeColor = PlaceholderForeColor;
            }
        }

        bool ShouldShowPlaceHolderText()
        {
            return (!initializing && !string.IsNullOrWhiteSpace(PlaceholderText) && (IsShowingPlaceholderText() || string.IsNullOrWhiteSpace(base.Text)));
        }

        bool IsShowingPlaceholderText()
        {

            if (!string.IsNullOrWhiteSpace(PlaceholderText) && base.Text == PlaceholderText)
                return true;
            return false;
        }
    }
}
