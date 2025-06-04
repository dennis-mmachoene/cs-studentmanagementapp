namespace StudentManagementApp.ViewModels
{
    public class DataTableRequest
    {
        public int Draw { get; set; }
        public int Start { get; set; }
        public int Length { get; set; }
        public Search Search { get; set; } = new Search();
        public List<Column> Columns { get; set; } = new List<Column>();
        public List<Order> Order { get; set; } = new List<Order>();
    }

    public class Search
    {
        public string Value { get; set; } = string.Empty;
        public bool Regex { get; set; }
    }

    public class Column
    {
        public string Data { get; set; } = string.Empty;
        public string Name { get; set; } = string.Empty;
        public bool Searchable { get; set; }
        public Search Search { get; set; } = new Search();
    }

    public class Order
    {
        public int Column { get; set; }
        public string Dir { get; set; }
    }
}
