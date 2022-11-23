namespace ProCards.DAL.Exceptions;

public class CardAlreadyExistsException: Exception
{
    public CardAlreadyExistsException()
    {
        
    }

    public CardAlreadyExistsException(string message)
        :base(message)
    {
        
    }
}