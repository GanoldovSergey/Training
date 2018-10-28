namespace Training.DAL.Services.Interfaces
{
    public interface IConfigManager
    {
        string DatabaseId { get; }
        string UserCollectionId { get; }
        string TestCollectionId { get; }
        string QuestionCollectionId { get; }
        string Endpoint { get; }
        string AuthKey { get; }
    }
}
