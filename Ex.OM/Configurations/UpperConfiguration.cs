using System.Windows.Controls;

namespace Ex.OM.Configurations
{
    public static class UpperConfiguration
    {

        public static void AddUpper(this TextBox textBox)
        {
            textBox.Text = textBox.Text.ToUpper();
            textBox.CharacterCasing = CharacterCasing.Upper;
        }
        public static void RemoveUpper(this TextBox textBox)
        {
            textBox.CharacterCasing = CharacterCasing.Normal;
        }

    }
}
