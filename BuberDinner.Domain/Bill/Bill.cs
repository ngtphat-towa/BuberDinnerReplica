﻿using BuberDinner.Domain.Bill.ValueObjects;
using BuberDinner.Domain.Common.Models;
using BuberDinner.Domain.Dinner.ValueObjects;
using BuberDinner.Domain.Guest.ValueObjects;
using BuberDinner.Domain.Host.ValueObjects;


namespace BuberDinner.Domain.Bill;

public sealed class Bill : AggregateRoot<BillId, Guid>
{
    public DinnerId DinnerId { get; }
    public GuestId GuestId { get; }
    public HostId HostId { get; }
    public Price Price { get; }
    public DateTime CreatedDateTime { get; }
    public DateTime UpdatedDateTime { get; }

    private Bill(
        BillId id,
        GuestId guestId,
        HostId hostId,
        DinnerId dinnerId,
        Price price,
        DateTime createdDateTime,
        DateTime updatedDateTime)
        : base(id)
    {
        GuestId = guestId;
        HostId = hostId;
        DinnerId = dinnerId;
        Price = price;
        CreatedDateTime = createdDateTime;
        UpdatedDateTime = updatedDateTime;
    }

    public static Bill Create(
        GuestId guestId,
        HostId hostId,
        DinnerId dinnerId,
        Price price)
    {
        return new(
            BillId.CreateUnique(),
            guestId,
            hostId,
            dinnerId,
            price,
            DateTime.UtcNow,
            DateTime.UtcNow);
    }
}