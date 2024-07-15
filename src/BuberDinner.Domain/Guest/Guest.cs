using BuberDinner.Domain.Bill.ValueObjects;
using BuberDinner.Domain.Common.Models;
using BuberDinner.Domain.Common.ValueObjects;
using BuberDinner.Domain.Dinner.ValueObjects;
using BuberDinner.Domain.Guest.ValueObjects;
using BuberDinner.Domain.MenuReview.ValueObjects;
using BuberDinner.Domain.User.ValueObjects;

namespace BuberDinner.Domain.MenuReview
{
    public sealed class Guest : AggregateRoot<GuestId, Guid>
    {
    
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string ProfileImage { get; set; }
        public AverageRating AverageRating { get; set; }
        public UserId UserId { get; set; }
        public List<DinnerId> UpcomingDinnerIds { get; set; }
        public List<DinnerId> PastDinnerIds { get; set; }
        public List<DinnerId> PendingDinnerIds { get; set; }
        public List<BillId> BillIds { get; set; }
        public List<MenuReviewId> MenuReviewIds { get; set; }
        public List<Rating> Ratings { get; set; }
        public DateTime CreatedDateTime { get; set; }
        public DateTime UpdatedDateTime { get; set; }

        public Guest(GuestId id,
                 string firstName,
                 string lastName,
                 string profileImage,
                 AverageRating averageRating,
                 UserId userId,
                 List<DinnerId> upcomingDinnerIds,
                 List<DinnerId> pastDinnerIds,
                 List<DinnerId> pendingDinnerIds,
                 List<BillId> billIds,
                 List<MenuReviewId> menuReviewIds,
                 List<Rating> ratings,
                 DateTime createdDateTime,
                 DateTime updatedDateTime) : base(id)
        {
            FirstName = firstName;
            LastName = lastName;
            ProfileImage = profileImage;
            AverageRating = averageRating;
            UserId = userId;
            UpcomingDinnerIds = upcomingDinnerIds;
            PastDinnerIds = pastDinnerIds;
            PendingDinnerIds = pendingDinnerIds;
            BillIds = billIds;
            MenuReviewIds = menuReviewIds;
            Ratings = ratings;
            CreatedDateTime = createdDateTime;
            UpdatedDateTime = updatedDateTime;
        }

        public static Guest Create(string firstName,
                 string lastName,
                 string profileImage,
                 AverageRating averageRating,
                 UserId userId,
                 List<DinnerId> upcomingDinnerIds,
                 List<DinnerId> pastDinnerIds,
                 List<DinnerId> pendingDinnerIds,
                 List<BillId> billIds,
                 List<MenuReviewId> menuReviewIds,
                 List<Rating> ratings,
                 DateTime createdDateTime,
                 DateTime updatedDateTime)
        {
            return new Guest(GuestId.CreateUnique(),
            firstName,
            lastName,
            profileImage,
            averageRating,
            userId,
            upcomingDinnerIds,
            pastDinnerIds,
            pendingDinnerIds,
            billIds,
            menuReviewIds,
            ratings,
            createdDateTime,
            updatedDateTime);
        }
    } 
}
