namespace AppCreator.Data {
    public class BaseDbObject {
        [PrimaryKey, AutoIncrement]
        public int Id { get; set; }
    }
}