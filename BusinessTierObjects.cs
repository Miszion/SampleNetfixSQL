 //
  // Given a user object, returns details about this user --- reviews, average 
  // rating given, etc.
  //
  // NOTE: the reviews are returned in order by rating (descending 5, 4, 3, ...),
  // with secondary sort based on movie id (ascending).
  //
  public class UserDetail
  {
    public readonly User user;
    public readonly double AvgRating;
    public readonly int NumReviews;
    public readonly IReadOnlyList<Review> Reviews;

    public UserDetail(User u, double avgRating, int numReviews, IReadOnlyList<Review> reviews)
    {
      user = u;
      AvgRating = avgRating;
      NumReviews = numReviews;
      Reviews = reviews;
    }
  }

}//namespace

