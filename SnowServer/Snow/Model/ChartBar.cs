namespace Snow.Model
{
    /// <summary>
    /// Object for data delivered by the user
    /// </summary>
    public class ChartBar
    {
        public ChartBar(string name, string color, int value)
        {
            this.Name = name;
            this.Color = color;
            this.Value = value;
        }

        public string Color { get; set; }

        public string Name { get; set; }

        public int Value { get; set; }
    }
}