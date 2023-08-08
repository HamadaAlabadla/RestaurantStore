namespace RestauranteStore.Core.Error
{
	public class ErrorModel
	{
		public ErrorModel()
		{
			errors = new List<ErrorMessage>();
		}
		public List<ErrorMessage> errors;
		public void AddError(string message)
		{
			errors.Add(new ErrorMessage(message));
		}
	}
	public class ErrorMessage
	{
		public ErrorMessage(string message)
		{
			Message = message;
		}
		public string? Message { get; set; }
	}
}