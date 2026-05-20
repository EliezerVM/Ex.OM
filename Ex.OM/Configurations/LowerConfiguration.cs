using System.Windows.Controls;

namespace Ex.OM.Configurations
{
    public static class LowerConfiguration
    {
        public static void AddLower(this TextBox textBox)
        {
            textBox.Text = textBox.Text.ToLower();
            textBox.CharacterCasing = CharacterCasing.Lower;
        }
        public static void RemoveLower(this TextBox textBox)
        {
            textBox.CharacterCasing = CharacterCasing.Normal;
        }
     
    }
}
