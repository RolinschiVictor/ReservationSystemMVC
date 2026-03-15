using ReservationSystemMVC.Core.Abstractions.Repositories;
using ReservationSystemMVC.Core.Domain.Builders;
using ReservationSystemMVC.Core.Domain.Entities;
using ReservationSystemMVC.Core.Domain.Enums;
using ReservationSystemMVC.Core.Domain.ValueObjects;

namespace ReservationSystemMVC.Infrastructure;

/// <summary>
/// Seeds the in-memory repository with realistic data using:
///   - BUILDER PATTERN: constructs complex objects step-by-step
///   - PROTOTYPE PATTERN: clones existing resources to create variations
/// </summary>
public static class DataSeeder
{
    public static void Seed(IResourceRepository repo)
    {
        SeedHotels(repo);
        SeedApartments(repo);
        SeedEvents(repo);
    }

    private static void SeedHotels(IResourceRepository repo)
    {
        // ══════════════════════════════════════════════════════
        // BUILDER PATTERN: construct complex hotels fluently
        // ══════════════════════════════════════════════════════

        // ── 1. JW Marriott Bucharest (built with Builder) ──
        var marriott = new HotelRoomBuilder()
            .SetName("JW Marriott Bucharest Grand Hotel")
            .SetBeds(2).SetPricePerDay(190m)
            .SetDescription("Situat în centrul Bucureștiului, lângă Palatul Parlamentului, JW Marriott oferă camere elegante, spa de lux și restaurante premiate.")
            .SetLocation("București", "Calea 13 Septembrie 90", "România")
            .SetRating(4.7)
            .AddImage("https://images.unsplash.com/photo-1566073771259-6a8506099945?w=600")
            .AddRoomType("Deluxe King", 2, 190m, "35 m²", "King bed", "City view")
            .AddRoomType("Executive Suite", 2, 350m, "55 m²", "King bed", "Living area", "Lounge access")
            .AddRoomType("Superior Twin", 2, 170m, "30 m²", "2 Twin beds", "Desk")
            .SetRoomFeatures(RoomFeature.WiFi | RoomFeature.AirConditioning | RoomFeature.TV | RoomFeature.PrivateBathroom | RoomFeature.MiniBar | RoomFeature.Desk | RoomFeature.CoffeeMachine)
            .SetAmenities(HotelAmenity.FreeWiFi | HotelAmenity.Parking | HotelAmenity.Restaurant | HotelAmenity.Bar | HotelAmenity.Spa | HotelAmenity.SwimmingPool | HotelAmenity.Gym | HotelAmenity.RoomService | HotelAmenity.Reception24h)
            .SetStyle(HotelStyle.Luxury | HotelStyle.Business)
            .AddLanguages("English", "Romanian", "French", "German")
            .AddReview("Maria P.", 5.0, "Camere impecabile, personal foarte amabil. Micul dejun excelent!", new DateOnly(2025, 3, 15))
            .AddReview("Alex T.", 4.5, "Locație perfectă, spa-ul este fenomenal.", new DateOnly(2025, 5, 2))
            .AddReview("John S.", 4.7, "Great business hotel, excellent meeting rooms.", new DateOnly(2025, 4, 20))
            .Build();
        repo.Add(marriott);

        // ── 2. Athenee Palace Hilton (built with Builder) ──
        var hilton = new HotelRoomBuilder()
            .SetName("Athenee Palace Hilton Bucharest")
            .SetBeds(2).SetPricePerDay(210m)
            .SetDescription("Hotel istoric 5 stele, situat pe Calea Victoriei, vizavi de Ateneul Român. Elegantă clasică îmbinată cu confort modern.")
            .SetLocation("București", "Str. Episcopiei 1-3", "România")
            .SetRating(4.6)
            .AddImage("https://images.unsplash.com/photo-1551882547-ff40c63fe5fa?w=600")
            .AddRoomType("Executive Room", 2, 210m, "32 m²", "King bed", "Atheneum view")
            .AddRoomType("Deluxe Suite", 2, 420m, "60 m²", "King bed", "Separate living room")
            .AddRoomType("Single Room", 1, 160m, "22 m²", "Single bed", "Work desk")
            .SetRoomFeatures(RoomFeature.WiFi | RoomFeature.AirConditioning | RoomFeature.TV | RoomFeature.PrivateBathroom | RoomFeature.MiniBar | RoomFeature.CoffeeMachine)
            .SetAmenities(HotelAmenity.FreeWiFi | HotelAmenity.Restaurant | HotelAmenity.Bar | HotelAmenity.Spa | HotelAmenity.Gym | HotelAmenity.RoomService | HotelAmenity.Reception24h)
            .SetStyle(HotelStyle.Luxury | HotelStyle.Romantic)
            .AddLanguages("English", "Romanian", "French")
            .AddReview("Elena R.", 4.8, "Locație de vis, chiar lângă Ateneu. Camera cu vedere superbă.", new DateOnly(2025, 2, 10))
            .AddReview("David M.", 4.3, "Beautiful historic hotel but rooms could use some updating.", new DateOnly(2025, 6, 1))
            .Build();
        repo.Add(hilton);

        // ── 3. Radisson Blu (built with Builder) ──
        var radisson = new HotelRoomBuilder()
            .SetName("Radisson Blu Hotel Bucharest")
            .SetBeds(2).SetPricePerDay(170m)
            .SetDescription("Hotel modern în Calea Victoriei, oferind camere confortabile, restaurant cu bucătărie internațională și facilități business.")
            .SetLocation("București", "Calea Victoriei 63-81", "România")
            .SetRating(4.4)
            .AddImage("https://images.unsplash.com/photo-1542314831-068cd1dbfeeb?w=600")
            .AddRoomType("Superior Room", 2, 170m, "28 m²", "Queen bed", "City view")
            .AddRoomType("Business Class Room", 2, 220m, "32 m²", "King bed", "Nespresso", "Lounge")
            .SetRoomFeatures(RoomFeature.WiFi | RoomFeature.AirConditioning | RoomFeature.TV | RoomFeature.PrivateBathroom | RoomFeature.Desk)
            .SetAmenities(HotelAmenity.FreeWiFi | HotelAmenity.Parking | HotelAmenity.Restaurant | HotelAmenity.Bar | HotelAmenity.Gym | HotelAmenity.RoomService | HotelAmenity.Reception24h)
            .SetStyle(HotelStyle.Business | HotelStyle.FamilyFriendly)
            .AddLanguages("English", "Romanian", "German")
            .AddReview("Andrei C.", 4.5, "Raport calitate-preț excelent. Locație centrală.", new DateOnly(2025, 4, 8))
            .AddReview("Sophie L.", 4.2, "Clean rooms, nice breakfast buffet.", new DateOnly(2025, 5, 22))
            .Build();
        repo.Add(radisson);

        // ── 4. InterContinental Paris (built with Builder) ──
        var intercontinental = new HotelRoomBuilder()
            .SetName("InterContinental Paris - Le Grand")
            .SetBeds(2).SetPricePerDay(420m)
            .SetDescription("Situat vizavi de Opera Garnier, acest hotel legendar din Paris oferă un amestec unic de istorie și lux modern.")
            .SetLocation("Paris", "2 Rue Scribe, 75009", "Franța")
            .SetRating(4.8)
            .AddImage("https://images.unsplash.com/photo-1445019980597-93fa8acb246c?w=600")
            .AddRoomType("Classic Room", 2, 420m, "30 m²", "Queen bed", "Opera view")
            .AddRoomType("Deluxe Room", 2, 580m, "40 m²", "King bed", "Marble bathroom")
            .AddRoomType("Presidential Suite", 4, 2200m, "120 m²", "2 bedrooms", "Grand piano", "Butler service")
            .SetRoomFeatures(RoomFeature.WiFi | RoomFeature.AirConditioning | RoomFeature.TV | RoomFeature.PrivateBathroom | RoomFeature.Balcony | RoomFeature.MiniBar | RoomFeature.Desk | RoomFeature.CoffeeMachine)
            .SetAmenities(HotelAmenity.FreeWiFi | HotelAmenity.Restaurant | HotelAmenity.Bar | HotelAmenity.Spa | HotelAmenity.Gym | HotelAmenity.RoomService | HotelAmenity.Reception24h | HotelAmenity.AirportShuttle)
            .SetStyle(HotelStyle.Luxury | HotelStyle.Romantic)
            .AddLanguages("English", "French", "German", "Russian")
            .AddReview("Pierre D.", 5.0, "Magnifique! Le meilleur hôtel de Paris.", new DateOnly(2025, 1, 20))
            .AddReview("Anna K.", 4.6, "Absolutely stunning. The Opera view is breathtaking.", new DateOnly(2025, 3, 5))
            .Build();
        repo.Add(intercontinental);

        // ── 5. Ritz Paris (built with Builder) ──
        var ritz = new HotelRoomBuilder()
            .SetName("Ritz Paris")
            .SetBeds(2).SetPricePerDay(950m)
            .SetDescription("Hotelul legendar de pe Place Vendôme. Epitomul luxului parizian din 1898, renovat cu grijă pentru a îmbina tradiția cu modernul.")
            .SetLocation("Paris", "15 Place Vendôme, 75001", "Franța")
            .SetRating(4.9)
            .AddImage("https://images.unsplash.com/photo-1582719478250-c89cae4dc85b?w=600")
            .AddRoomType("Deluxe Room", 2, 950m, "40 m²", "King bed", "Vendôme view")
            .AddRoomType("Prestige Suite", 2, 2500m, "75 m²", "King bed", "Salon privat")
            .AddRoomType("Suite Coco Chanel", 2, 18000m, "188 m²", "Historic suite", "Private terrace")
            .SetRoomFeatures(RoomFeature.WiFi | RoomFeature.AirConditioning | RoomFeature.TV | RoomFeature.PrivateBathroom | RoomFeature.Balcony | RoomFeature.MiniBar | RoomFeature.Desk | RoomFeature.CoffeeMachine)
            .SetAmenities(HotelAmenity.FreeWiFi | HotelAmenity.Restaurant | HotelAmenity.Bar | HotelAmenity.Spa | HotelAmenity.SwimmingPool | HotelAmenity.Gym | HotelAmenity.RoomService | HotelAmenity.Reception24h | HotelAmenity.AirportShuttle)
            .SetStyle(HotelStyle.Luxury | HotelStyle.Romantic | HotelStyle.Boutique)
            .AddLanguages("English", "French", "Russian", "German")
            .AddReview("Catherine B.", 5.0, "Un rêve devenu réalité. Service impecabil.", new DateOnly(2025, 2, 14))
            .AddReview("Mark W.", 4.9, "Once in a lifetime experience. Worth every euro.", new DateOnly(2025, 4, 30))
            .Build();
        repo.Add(ritz);

        // ══════════════════════════════════════════════════════
        // PROTOTYPE PATTERN: clone Radisson to create a Sheraton
        // (same amenities/features, different name/location/price)
        // ══════════════════════════════════════════════════════
        var sheraton = (HotelRoom)radisson.Clone();
        sheraton.Name = "Sheraton Bucharest Hotel";
        sheraton.Description = "Hotel de 5 stele în centrul Bucureștiului, ideal pentru călătorii de afaceri, cu săli de conferință moderne și restaurant internațional.";
        sheraton.Location = new Location("București", "Calea Dorobanților 5-7", "România");
        sheraton.Rating = 4.3;
        sheraton.Images = ["https://images.unsplash.com/photo-1564501049412-61c2a3083791?w=600"];
        sheraton.RoomTypes =
        [
            new RoomType("Classic Room", 2, 165m, "28 m²", ["Queen bed"]),
            new RoomType("Club Room", 2, 230m, "32 m²", ["King bed", "Club lounge access"])
        ];
        sheraton.Style = HotelStyle.Business;
        sheraton.LanguagesSpoken = ["English", "Romanian"];
        sheraton.Reviews =
        [
            new Review("Mihai L.", 4.4, "Foarte bun pentru business. Sălile de conferință sunt excelente.", new DateOnly(2025, 3, 12))
        ];
        repo.Add(sheraton);
    }

    private static void SeedApartments(IResourceRepository repo)
    {
        // ══════════════════════════════════════════════════════
        // BUILDER PATTERN: apartments built fluently
        // ══════════════════════════════════════════════════════

        var oldTown = new ApartmentBuilder()
            .SetName("Central Old Town Apartment")
            .SetRooms(2).SetPricePerDay(85m)
            .SetDescription("Apartament modern în Centrul Vechi al Bucureștiului, la 2 minute de Curtea Veche. Ideal pentru cupluri și turiști.")
            .SetLocation("București", "Str. Franceza 18", "România")
            .SetRating(4.6)
            .AddImage("https://images.unsplash.com/photo-1502672260266-1c1ef2d93688?w=600")
            .SetFeatures(ApartmentFeature.Kitchen | ApartmentFeature.WiFi | ApartmentFeature.AirConditioning | ApartmentFeature.TV | ApartmentFeature.WashingMachine | ApartmentFeature.Balcony)
            .SetBeds(2).SetBathrooms(1).SetArea("55 m²").SetMaxGuests(4)
            .SetRules(new TimeOnly(14, 0), new TimeOnly(11, 0), false, false, false)
            .AddReview("Ioana M.", 4.8, "Locație excelentă, apartament curat și modern. Recomand!", new DateOnly(2025, 4, 5))
            .AddReview("Thomas H.", 4.4, "Perfect location for exploring Old Town. Kitchen well equipped.", new DateOnly(2025, 5, 18))
            .Build();
        repo.Add(oldTown);

        var herastrau = new ApartmentBuilder()
            .SetName("Herăstrău Park View Apartment")
            .SetRooms(3).SetPricePerDay(140m)
            .SetDescription("Apartament spațios cu vedere spre Parcul Herăstrău, 3 camere, parcare inclusă. Perfect pentru familii.")
            .SetLocation("București", "Bd. Aviatorilor 52", "România")
            .SetRating(4.7)
            .AddImage("https://images.unsplash.com/photo-1522708323590-d24dbb6b0267?w=600")
            .SetFeatures(ApartmentFeature.Kitchen | ApartmentFeature.WiFi | ApartmentFeature.AirConditioning | ApartmentFeature.TV | ApartmentFeature.WashingMachine | ApartmentFeature.Balcony | ApartmentFeature.Parking | ApartmentFeature.Elevator)
            .SetBeds(4).SetBathrooms(2).SetArea("90 m²").SetMaxGuests(6)
            .SetRules(new TimeOnly(15, 0), new TimeOnly(11, 0), true, false, false)
            .AddReview("Cristina D.", 4.9, "Vedere superbă, copiii au adorat parcul din apropiere!", new DateOnly(2025, 6, 2))
            .AddReview("James R.", 4.5, "Spacious, clean, and the park is a bonus. Great for families.", new DateOnly(2025, 5, 10))
            .Build();
        repo.Add(herastrau);

        // ══════════════════════════════════════════════════════
        // PROTOTYPE PATTERN: clone Old Town apartment for Cluj
        // (same features/rules, different city/price)
        // ══════════════════════════════════════════════════════
        var clujApt = (Apartment)oldTown.Clone();
        clujApt.Name = "City Center Apartment Cluj-Napoca";
        clujApt.Description = "Studio modern în centrul Clujului, aproape de Piața Unirii și teatru. Ideal pentru city break.";
        clujApt.Location = new Location("Cluj-Napoca", "Str. Napoca 12", "România");
        clujApt.Rating = 4.5;
        clujApt.Images = ["https://images.unsplash.com/photo-1560448204-e02f11c3d0e2?w=600"];
        clujApt.Area = "45 m²";
        clujApt.MaxGuests = 3;
        clujApt.Reviews = [new Review("Radu B.", 4.6, "Foarte curat, locație centrală perfectă.", new DateOnly(2025, 3, 28))];
        repo.Add(clujApt);

        var viennaApt = new ApartmentBuilder()
            .SetName("Vienna Ringstrasse Apartment")
            .SetRooms(2).SetPricePerDay(160m)
            .SetDescription("Apartament elegant pe celebra Ringstrasse, cu vedere spre Opera de Stat. Mobilat cu stil vienez clasic.")
            .SetLocation("Viena", "Opernring 11", "Austria")
            .SetRating(4.8)
            .AddImage("https://images.unsplash.com/photo-1493809842364-78817add7ffb?w=600")
            .SetFeatures(ApartmentFeature.Kitchen | ApartmentFeature.WiFi | ApartmentFeature.AirConditioning | ApartmentFeature.TV | ApartmentFeature.WashingMachine | ApartmentFeature.Elevator)
            .SetBeds(3).SetBathrooms(1).SetArea("70 m²").SetMaxGuests(4)
            .SetRules(new TimeOnly(15, 0), new TimeOnly(11, 0), false, false, false)
            .AddReview("Lukas W.", 4.9, "Wunderschön! Perfekte Lage direkt an der Oper.", new DateOnly(2025, 2, 20))
            .AddReview("Maria S.", 4.7, "Frumos, curat, vedere incredibilă. Merită fiecare euro.", new DateOnly(2025, 4, 15))
            .Build();
        repo.Add(viennaApt);
    }

    private static void SeedEvents(IResourceRepository repo)
    {
        // ══════════════════════════════════════════════════════
        // BUILDER PATTERN: events built fluently
        // ══════════════════════════════════════════════════════

        var romexpo = new EventVenueBuilder()
            .SetName("ROMEXPO Pavilion B1")
            .SetCapacity(2000).SetPricePerDay(1200m)
            .SetDescription("Cel mai mare centru expozițional din România. Pavilionul B1 este ideal pentru expoziții, târguri și conferințe de amploare.")
            .SetCategory(EventCategory.Exhibition)
            .SetLocation("București", "Bd. Mărăști 65-67", "România")
            .SetEventDate(new DateOnly(2026, 9, 15))
            .SetTimes(new TimeOnly(9, 0), new TimeOnly(18, 0))
            .SetOrganizer("ROMEXPO SA").SetDuration("3 zile")
            .AddImage("https://images.unsplash.com/photo-1540575467063-178a50c2df87?w=600")
            .SetFacilities(EventFacility.Parking | EventFacility.Food | EventFacility.Bar | EventFacility.SeatsAvailable)
            .AddReview("Vlad N.", 4.3, "Spațiu generos, organizare bună. Parcarea e aglomerată.", new DateOnly(2025, 10, 5))
            .Build();
        repo.Add(romexpo);

        var salaPalatului = new EventVenueBuilder()
            .SetName("Sala Palatului")
            .SetCapacity(4000).SetPricePerDay(2500m)
            .SetDescription("Sala emblematică a Bucureștiului, cu acustică excepțională. Găzduiește concerte, spectacole și evenimente culturale de top.")
            .SetCategory(EventCategory.Concert)
            .SetLocation("București", "Str. Ion Câmpineanu 28", "România")
            .SetEventDate(new DateOnly(2026, 7, 20))
            .SetTimes(new TimeOnly(19, 0), new TimeOnly(22, 30))
            .SetOrganizer("Events International").SetDuration("3.5 ore").SetAgeRestriction("12+")
            .AddImage("https://images.unsplash.com/photo-1459749411175-04bf5292ceea?w=600")
            .SetFacilities(EventFacility.Parking | EventFacility.Bar | EventFacility.VIPArea | EventFacility.SeatsAvailable)
            .AddReview("Ana V.", 4.8, "Acustică incredibilă! Experiență de neuitat.", new DateOnly(2025, 11, 12))
            .AddReview("Cristian M.", 4.6, "Locurile VIP merită investiția.", new DateOnly(2025, 12, 1))
            .Build();
        repo.Add(salaPalatului);

        var clujArena = new EventVenueBuilder()
            .SetName("Cluj Arena")
            .SetCapacity(25000).SetPricePerDay(8000m)
            .SetDescription("Stadionul multifuncțional din Cluj-Napoca, perfect pentru concerte mari, festivaluri și evenimente sportive.")
            .SetCategory(EventCategory.Festival)
            .SetLocation("Cluj-Napoca", "Aleea Stadionului 2", "România")
            .SetEventDate(new DateOnly(2026, 8, 5))
            .SetTimes(new TimeOnly(16, 0), new TimeOnly(23, 59))
            .SetOrganizer("Untold Festival").SetDuration("4 zile").SetAgeRestriction("16+")
            .AddImage("https://images.unsplash.com/photo-1493225457124-a3eb161ffa5f?w=600")
            .SetFacilities(EventFacility.Parking | EventFacility.Food | EventFacility.Bar | EventFacility.VIPArea | EventFacility.SeatsAvailable)
            .AddReview("Diana P.", 5.0, "Untold este cel mai bun festival din Europa!", new DateOnly(2025, 8, 10))
            .AddReview("Martin K.", 4.7, "Amazing atmosphere, well organized.", new DateOnly(2025, 8, 12))
            .Build();
        repo.Add(clujArena);

        // ══════════════════════════════════════════════════════
        // PROTOTYPE PATTERN: clone Sala Palatului for Iasi venue
        // (same facilities/category template, different location)
        // ══════════════════════════════════════════════════════
        var iasiVenue = (EventVenue)salaPalatului.Clone();
        iasiVenue.Name = "Palatul Culturii Iași";
        iasiVenue.Description = "Monument istoric și centru cultural, ideal pentru conferințe, spectacole de teatru și evenimente academice.";
        iasiVenue.Category = EventCategory.Conference;
        iasiVenue.Location = new Location("Iași", "Bd. Ștefan cel Mare și Sfânt 1", "România");
        iasiVenue.EventDate = new DateOnly(2026, 10, 10);
        iasiVenue.Organizer = "Primăria Iași";
        iasiVenue.Duration = "2 zile";
        iasiVenue.AgeRestriction = null;
        iasiVenue.Images = ["https://images.unsplash.com/photo-1505373877841-8d25f7d46678?w=600"];
        iasiVenue.Reviews = [new Review("Gabriela I.", 4.5, "Atmosferă specială, clădirea este impresionantă.", new DateOnly(2025, 10, 20))];
        repo.Add(iasiVenue);
    }
}
