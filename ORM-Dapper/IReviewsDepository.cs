namespace ORM_Dapper;

public interface IReviewsDepository
{
    IEnumerable<Reviews> GetAllReivews();
}