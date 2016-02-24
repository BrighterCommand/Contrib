﻿using System;
using FakeItEasy;
using Machine.Specifications;
using Paramore.Adapters.Tests.UnitTests.fakes;
using Paramore.Rewind.Core.Adapters.Repositories;
using Paramore.Rewind.Core.Domain.Venues;
using Paramore.Rewind.Core.Ports.Commands.Venue;
using Paramore.Rewind.Core.Ports.Handlers.Venues;

namespace Paramore.Adapters.Tests.UnitTests.services.CommandHandlers.Venues
{
    [Subject("A call to the add new venue handler should result in a new venue being added")]
    public class When_adding_a_new_venue
    {
        static AddVenueCommandHandler addVenueCommandHandler;
        static AddVenueCommand addVenueCommand;
        static FakeRepository<Venue, VenueDocument> venueRepository; 
        static IAmAUnitOfWorkFactory uoWFactory;
        static IUnitOfWork uow;
          
        Establish context = () =>
        {
            venueRepository = new FakeRepository<Venue, VenueDocument>();
            uoWFactory = A.Fake<IAmAUnitOfWorkFactory>();
            uow = A.Fake<IUnitOfWork>();

            A.CallTo(() => uoWFactory.CreateUnitOfWork()).Returns(uow);

            addVenueCommand = new AddVenueCommand(
                venueName: "My Venue Name", 
                address: "StreetNumber: 1, Street: MyStreet, City: London, PostCode: SW1 1PL",
                mapURN: "http://www.mysite.com/maps/12345",
                contact: "Name: Mary Alice, EmailAddress: mary.alice@foobar.com: , PhoneNumber: 0111 111 1111");

            addVenueCommandHandler = new AddVenueCommandHandler(venueRepository, uoWFactory);
        };

        static Venue GetVenueFromRepoBy(Guid id)
        {
            return venueRepository[id];
        }

        Because of = () => addVenueCommandHandler.Handle(addVenueCommand);

        It should_add_a_venue_to_the_repository = () => GetVenueFromRepoBy(addVenueCommand.Id).ShouldNotBeNull();
        It should_ask_the_session_factory_for_a_unit_of_work = () => A.CallTo(() => uoWFactory.CreateUnitOfWork()).MustHaveHappened();
        It should_commit_the_unit_of_work = () => A.CallTo(() => uow.Commit()).MustHaveHappened();
        It should_set_the_name_of_the_venue = () => GetVenueFromRepoBy(addVenueCommand.Id).ToDocument().VenueName.ShouldEqual(addVenueCommand.VenueName);
        It should_set_the_address_of_the_venue = () => GetVenueFromRepoBy(addVenueCommand.Id).ToDocument().Address.ShouldEqual(addVenueCommand.Address);
        It should_set_the_mapURN_for_the_venue = () => GetVenueFromRepoBy(addVenueCommand.Id).ToDocument().VenueMap.ShouldEqual(addVenueCommand.VenueMap);
        It should_set_the_contact_for_the_venue = () => GetVenueFromRepoBy(addVenueCommand.Id).ToDocument().VenueContact.ShouldEqual(addVenueCommand.Contact);


    }
}
