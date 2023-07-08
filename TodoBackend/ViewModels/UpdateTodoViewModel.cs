namespace TodoBackend.ViewModels
{
  public class UpdateTodoViewModel
  {
    public required string Title { get; set; }
    public required string Content { get; set; }
    public bool Done { get; set; }
  }
}
