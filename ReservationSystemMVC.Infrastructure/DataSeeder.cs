using ReservationSystemMVC.Core.Abstractions.Repositories;
using ReservationSystemMVC.Core.Domain.Entities;
using ReservationSystemMVC.Core.Domain.Enums;
using ReservationSystemMVC.Core.Domain.Factories.FactoryMethod;
using ReservationSystemMVC.Core.Domain.ValueObjects;

namespace ReservationSystemMVC.Infrastructure;

/// <summary>
/// Seeds the in-memory repository with realistic, professional data
/// using the Factory Method pattern.
/// </summary>
public static class DataSeeder
{
    public static void Seed(IResourceRepository repo)
    {
        ResourceFactoryMethod hotelFactory = new HotelRoomFactory();
        ResourceFactoryMethod apartmentFactory = new ApartmentFactory();
        ResourceFactoryMethod eventFactory = new EventVenueFactory();

        SeedHotels(repo, hotelFactory);
        SeedApartments(repo, apartmentFactory);
        SeedEvents(repo, eventFactory);
    }

    private static void SeedHotels(IResourceRepository repo, ResourceFactoryMethod factory)
    {
        // ── 1. JW Marriott Bucharest ──
        var h1 = (HotelRoom)factory.CreateResource("JW Marriott Bucharest Grand Hotel", 2, 190m);
        h1.Description = "Situat în centrul Bucureștiului, lângă Palatul Parlamentului, JW Marriott oferă camere elegante, spa de lux și restaurante premiate.";
        h1.Location = new Location("București", "Calea 13 Septembrie 90", "România");
        h1.Rating = 4.7;
        h1.Images = ["https://images.unsplash.com/photo-1566073771259-6a8506099945?w=600"];
        h1.RoomTypes =
        [
            new RoomType("Deluxe King", 2, 190m, "35 m²", ["King bed", "City view"]),
            new RoomType("Executive Suite", 2, 350m, "55 m²", ["King bed", "Living area", "Lounge access"]),
            new RoomType("Superior Twin", 2, 170m, "30 m²", ["2 Twin beds", "Desk"])
        ];
        h1.RoomFeatures = RoomFeature.WiFi | RoomFeature.AirConditioning | RoomFeature.TV | RoomFeature.PrivateBathroom | RoomFeature.MiniBar | RoomFeature.Desk | RoomFeature.CoffeeMachine;
        h1.Amenities = HotelAmenity.FreeWiFi | HotelAmenity.Parking | HotelAmenity.Restaurant | HotelAmenity.Bar | HotelAmenity.Spa | HotelAmenity.SwimmingPool | HotelAmenity.Gym | HotelAmenity.RoomService | HotelAmenity.Reception24h;
        h1.Style = HotelStyle.Luxury | HotelStyle.Business;
        h1.LanguagesSpoken = ["English", "Romanian", "French", "German"];
        h1.Reviews =
        [
            new Review("Maria P.", 5.0, "Camere impecabile, personal foarte amabil. Micul dejun excelent!", new DateOnly(2025, 3, 15)),
            new Review("Alex T.", 4.5, "Locație perfectă, spa-ul este fenomenal.", new DateOnly(2025, 5, 2)),
            new Review("John S.", 4.7, "Great business hotel, excellent meeting rooms.", new DateOnly(2025, 4, 20))
        ];
        repo.Add(h1);

        // ── 2. Athenee Palace Hilton ──
        var h2 = (HotelRoom)factory.CreateResource("Athenee Palace Hilton Bucharest", 2, 210m);
        h2.Description = "Hotel istoric 5 stele, situat pe Calea Victoriei, vizavi de Ateneul Român. Elegantă clasică îmbinată cu confort modern.";
        h2.Location = new Location("București", "Str. Episcopiei 1-3", "România");
        h2.Rating = 4.6;
        h2.Images = ["https://images.unsplash.com/photo-1551882547-ff40c63fe5fa?w=600"];
        h2.RoomTypes =
        [
            new RoomType("Executive Room", 2, 210m, "32 m²", ["King bed", "Atheneum view"]),
            new RoomType("Deluxe Suite", 2, 420m, "60 m²", ["King bed", "Separate living room"]),
            new RoomType("Single Room", 1, 160m, "22 m²", ["Single bed", "Work desk"])
        ];
        h2.RoomFeatures = RoomFeature.WiFi | RoomFeature.AirConditioning | RoomFeature.TV | RoomFeature.PrivateBathroom | RoomFeature.MiniBar | RoomFeature.CoffeeMachine;
        h2.Amenities = HotelAmenity.FreeWiFi | HotelAmenity.Restaurant | HotelAmenity.Bar | HotelAmenity.Spa | HotelAmenity.Gym | HotelAmenity.RoomService | HotelAmenity.Reception24h;
        h2.Style = HotelStyle.Luxury | HotelStyle.Romantic;
        h2.LanguagesSpoken = ["English", "Romanian", "French"];
        h2.Reviews =
        [
            new Review("Elena R.", 4.8, "Locație de vis, chiar lângă Ateneu. Camera cu vedere superbă.", new DateOnly(2025, 2, 10)),
            new Review("David M.", 4.3, "Beautiful historic hotel but rooms could use some updating.", new DateOnly(2025, 6, 1))
        ];
        repo.Add(h2);

        // ── 3. Radisson Blu Bucharest ──
        var h3 = (HotelRoom)factory.CreateResource("Radisson Blu Hotel Bucharest", 2, 170m);
        h3.Description = "Hotel modern în Calea Victoriei, oferind camere confortabile, restaurant cu bucătărie internațională și facilități business.";
        h3.Location = new Location("București", "Calea Victoriei 63-81", "România");
        h3.Rating = 4.4;
        h3.Images = ["https://images.unsplash.com/photo-1542314831-068cd1dbfeeb?w=600"];
        h3.RoomTypes =
        [
            new RoomType("Superior Room", 2, 170m, "28 m²", ["Queen bed", "City view"]),
            new RoomType("Business Class Room", 2, 220m, "32 m²", ["King bed", "Nespresso", "Lounge"]),
        ];
        h3.RoomFeatures = RoomFeature.WiFi | RoomFeature.AirConditioning | RoomFeature.TV | RoomFeature.PrivateBathroom | RoomFeature.Desk;
        h3.Amenities = HotelAmenity.FreeWiFi | HotelAmenity.Parking | HotelAmenity.Restaurant | HotelAmenity.Bar | HotelAmenity.Gym | HotelAmenity.RoomService | HotelAmenity.Reception24h;
        h3.Style = HotelStyle.Business | HotelStyle.FamilyFriendly;
        h3.LanguagesSpoken = ["English", "Romanian", "German"];
        h3.Reviews =
        [
            new Review("Andrei C.", 4.5, "Raport calitate-preț excelent. Locație centrală.", new DateOnly(2025, 4, 8)),
            new Review("Sophie L.", 4.2, "Clean rooms, nice breakfast buffet.", new DateOnly(2025, 5, 22))
        ];
        repo.Add(h3);

        // ── 4. InterContinental Athénée Paris ──
        var h4 = (HotelRoom)factory.CreateResource("InterContinental Paris - Le Grand", 2, 420m);
        h4.Description = "Situat vizavi de Opera Garnier, acest hotel legendar din Paris oferă un amestec unic de istorie și lux modern.";
        h4.Location = new Location("Paris", "2 Rue Scribe, 75009", "Franța");
        h4.Rating = 4.8;
        h4.Images = ["https://images.unsplash.com/photo-1445019980597-93fa8acb246c?w=600"];
        h4.RoomTypes =
        [
            new RoomType("Classic Room", 2, 420m, "30 m²", ["Queen bed", "Opera view"]),
            new RoomType("Deluxe Room", 2, 580m, "40 m²", ["King bed", "Marble bathroom"]),
            new RoomType("Presidential Suite", 4, 2200m, "120 m²", ["2 bedrooms", "Grand piano", "Butler service"])
        ];
        h4.RoomFeatures = RoomFeature.WiFi | RoomFeature.AirConditioning | RoomFeature.TV | RoomFeature.PrivateBathroom | RoomFeature.Balcony | RoomFeature.MiniBar | RoomFeature.Desk | RoomFeature.CoffeeMachine;
        h4.Amenities = HotelAmenity.FreeWiFi | HotelAmenity.Restaurant | HotelAmenity.Bar | HotelAmenity.Spa | HotelAmenity.Gym | HotelAmenity.RoomService | HotelAmenity.Reception24h | HotelAmenity.AirportShuttle;
        h4.Style = HotelStyle.Luxury | HotelStyle.Romantic;
        h4.LanguagesSpoken = ["English", "French", "German", "Russian"];
        h4.Reviews =
        [
            new Review("Pierre D.", 5.0, "Magnifique! Le meilleur hôtel de Paris.", new DateOnly(2025, 1, 20)),
            new Review("Anna K.", 4.6, "Absolutely stunning. The Opera view is breathtaking.", new DateOnly(2025, 3, 5))
        ];
        repo.Add(h4);

        // ── 5. Ritz Paris ──
        var h5 = (HotelRoom)factory.CreateResource("Ritz Paris", 2, 950m);
        h5.Description = "Hotelul legendar de pe Place Vendôme. Epitomul luxului parizian din 1898, renovat cu grijă pentru a îmbina tradiția cu modernul.";
        h5.Location = new Location("Paris", "15 Place Vendôme, 75001", "Franța");
        h5.Rating = 4.9;
        h5.Images = ["https://images.unsplash.com/photo-1582719478250-c89cae4dc85b?w=600"];
        h5.RoomTypes =
        [
            new RoomType("Deluxe Room", 2, 950m, "40 m²", ["King bed", "Vendôme view"]),
            new RoomType("Prestige Suite", 2, 2500m, "75 m²", ["King bed", "Salon privat"]),
            new RoomType("Suite Coco Chanel", 2, 18000m, "188 m²", ["Historic suite", "Private terrace"])
        ];
        h5.RoomFeatures = RoomFeature.WiFi | RoomFeature.AirConditioning | RoomFeature.TV | RoomFeature.PrivateBathroom | RoomFeature.Balcony | RoomFeature.MiniBar | RoomFeature.Desk | RoomFeature.CoffeeMachine;
        h5.Amenities = HotelAmenity.FreeWiFi | HotelAmenity.Restaurant | HotelAmenity.Bar | HotelAmenity.Spa | HotelAmenity.SwimmingPool | HotelAmenity.Gym | HotelAmenity.RoomService | HotelAmenity.Reception24h | HotelAmenity.AirportShuttle;
        h5.Style = HotelStyle.Luxury | HotelStyle.Romantic | HotelStyle.Boutique;
        h5.LanguagesSpoken = ["English", "French", "Russian", "German"];
        h5.Reviews =
        [
            new Review("Catherine B.", 5.0, "Un rêve devenu réalité. Service impeccable.", new DateOnly(2025, 2, 14)),
            new Review("Mark W.", 4.9, "Once in a lifetime experience. Worth every euro.", new DateOnly(2025, 4, 30))
        ];
        repo.Add(h5);

        // ── 6. Sheraton Bucharest ──
        var h6 = (HotelRoom)factory.CreateResource("Sheraton Bucharest Hotel", 2, 165m);
        h6.Description = "Hotel de 5 stele în centrul Bucureștiului, ideal pentru călătorii de afaceri, cu săli de conferință moderne și restaurant internațional.";
        h6.Location = new Location("București", "Calea Dorobanților 5-7", "România");
        h6.Rating = 4.3;
        h6.Images = ["https://images.unsplash.com/photo-1564501049412-61c2a3083791?w=600"];
        h6.RoomTypes =
        [
            new RoomType("Classic Room", 2, 165m, "28 m²", ["Queen bed"]),
            new RoomType("Club Room", 2, 230m, "32 m²", ["King bed", "Club lounge access"]),
        ];
        h6.RoomFeatures = RoomFeature.WiFi | RoomFeature.AirConditioning | RoomFeature.TV | RoomFeature.PrivateBathroom | RoomFeature.Desk;
        h6.Amenities = HotelAmenity.FreeWiFi | HotelAmenity.Parking | HotelAmenity.Restaurant | HotelAmenity.Bar | HotelAmenity.Gym | HotelAmenity.RoomService | HotelAmenity.Reception24h;
        h6.Style = HotelStyle.Business;
        h6.LanguagesSpoken = ["English", "Romanian"];
        h6.Reviews =
        [
            new Review("Mihai L.", 4.4, "Foarte bun pentru business. Sălile de conferință sunt excelente.", new DateOnly(2025, 3, 12))
        ];
        repo.Add(h6);
    }

    private static void SeedApartments(IResourceRepository repo, ResourceFactoryMethod factory)
    {
        // ── 1. Central Old Town Bucharest ──
        var a1 = (Apartment)factory.CreateResource("Central Old Town Apartment", 2, 85m);
        a1.Description = "Apartament modern în Centrul Vechi al Bucureștiului, la 2 minute de Curtea Veche. Ideal pentru cupluri și turiști.";
        a1.Location = new Location("București", "Str. Franceza 18", "România");
        a1.Rating = 4.6;
        a1.Images = ["https://images.unsplash.com/photo-1502672260266-1c1ef2d93688?w=600"];
        a1.Features = ApartmentFeature.Kitchen | ApartmentFeature.WiFi | ApartmentFeature.AirConditioning | ApartmentFeature.TV | ApartmentFeature.WashingMachine | ApartmentFeature.Balcony;
        a1.Beds = 2;
        a1.Bathrooms = 1;
        a1.Area = "55 m²";
        a1.MaxGuests = 4;
        a1.Rules = new ApartmentRules(new TimeOnly(14, 0), new TimeOnly(11, 0), false, false, false);
        a1.Reviews =
        [
            new Review("Ioana M.", 4.8, "Locație excelentă, apartament curat și modern. Recomand!", new DateOnly(2025, 4, 5)),
            new Review("Thomas H.", 4.4, "Perfect location for exploring Old Town. Kitchen well equipped.", new DateOnly(2025, 5, 18))
        ];
        repo.Add(a1);

        // ── 2. Herăstrău Park View ──
        var a2 = (Apartment)factory.CreateResource("Herăstrău Park View Apartment", 3, 140m);
        a2.Description = "Apartament spațios cu vedere spre Parcul Herăstrău, 3 camere, parcare inclusă. Perfect pentru familii.";
        a2.Location = new Location("București", "Bd. Aviatorilor 52", "România");
        a2.Rating = 4.7;
        a2.Images = ["https://images.unsplash.com/photo-1522708323590-d24dbb6b0267?w=600"];
        a2.Features = ApartmentFeature.Kitchen | ApartmentFeature.WiFi | ApartmentFeature.AirConditioning | ApartmentFeature.TV | ApartmentFeature.WashingMachine | ApartmentFeature.Balcony | ApartmentFeature.Parking | ApartmentFeature.Elevator;
        a2.Beds = 4;
        a2.Bathrooms = 2;
        a2.Area = "90 m²";
        a2.MaxGuests = 6;
        a2.Rules = new ApartmentRules(new TimeOnly(15, 0), new TimeOnly(11, 0), true, false, false);
        a2.Reviews =
        [
            new Review("Cristina D.", 4.9, "Vedere superbă, copiii au adorat parcul din apropiere!", new DateOnly(2025, 6, 2)),
            new Review("James R.", 4.5, "Spacious, clean, and the park is a bonus. Great for families.", new DateOnly(2025, 5, 10))
        ];
        repo.Add(a2);

        // ── 3. Cluj City Center ──
        var a3 = (Apartment)factory.CreateResource("City Center Apartment Cluj-Napoca", 2, 75m);
        a3.Description = "Studio modern în centrul Clujului, aproape de Piața Unirii și teatru. Ideal pentru city break.";
        a3.Location = new Location("Cluj-Napoca", "Str. Napoca 12", "România");
        a3.Rating = 4.5;
        a3.Images = ["https://images.unsplash.com/photo-1560448204-e02f11c3d0e2?w=600"];
        a3.Features = ApartmentFeature.Kitchen | ApartmentFeature.WiFi | ApartmentFeature.AirConditioning | ApartmentFeature.TV | ApartmentFeature.Elevator;
        a3.Beds = 2;
        a3.Bathrooms = 1;
        a3.Area = "45 m²";
        a3.MaxGuests = 3;
        a3.Rules = new ApartmentRules(new TimeOnly(14, 0), new TimeOnly(10, 0), false, false, false);
        a3.Reviews =
        [
            new Review("Radu B.", 4.6, "Foarte curat, locație centrală perfectă.", new DateOnly(2025, 3, 28))
        ];
        repo.Add(a3);

        // ── 4. Vienna Ring ──
        var a4 = (Apartment)factory.CreateResource("Vienna Ringstrasse Apartment", 2, 160m);
        a4.Description = "Apartament elegant pe celebra Ringstrasse, cu vedere spre Opera de Stat. Mobilat cu stil vienez clasic.";
        a4.Location = new Location("Viena", "Opernring 11", "Austria");
        a4.Rating = 4.8;
        a4.Images = ["https://images.unsplash.com/photo-1493809842364-78817add7ffb?w=600"];
        a4.Features = ApartmentFeature.Kitchen | ApartmentFeature.WiFi | ApartmentFeature.AirConditioning | ApartmentFeature.TV | ApartmentFeature.WashingMachine | ApartmentFeature.Elevator;
        a4.Beds = 3;
        a4.Bathrooms = 1;
        a4.Area = "70 m²";
        a4.MaxGuests = 4;
        a4.Rules = new ApartmentRules(new TimeOnly(15, 0), new TimeOnly(11, 0), false, false, false);
        a4.Reviews =
        [
            new Review("Lukas W.", 4.9, "Wunderschön! Perfekte Lage direkt an der Oper.", new DateOnly(2025, 2, 20)),
            new Review("Maria S.", 4.7, "Frumos, curat, vedere incredibilă. Merită fiecare euro.", new DateOnly(2025, 4, 15))
        ];
        repo.Add(a4);
    }

    private static void SeedEvents(IResourceRepository repo, ResourceFactoryMethod factory)
    {
        // ── 1. ROMEXPO ──
        var e1 = (EventVenue)factory.CreateResource("ROMEXPO Pavilion B1", 2000, 1200m);
        e1.Description = "Cel mai mare centru expozițional din România. Pavilionul B1 este ideal pentru expoziții, târguri și conferințe de amploare.";
        e1.Category = EventCategory.Exhibition;
        e1.Location = new Location("București", "Bd. Mărăști 65-67", "România");
        e1.EventDate = new DateOnly(2026, 9, 15);
        e1.StartTime = new TimeOnly(9, 0);
        e1.EndTime = new TimeOnly(18, 0);
        e1.Organizer = "ROMEXPO SA";
        e1.Duration = "3 zile";
        e1.AgeRestriction = null;
        e1.Images = ["https://images.unsplash.com/photo-1540575467063-178a50c2df87?w=600"];
        e1.Facilities = EventFacility.Parking | EventFacility.Food | EventFacility.Bar | EventFacility.SeatsAvailable;
        e1.Reviews =
        [
            new Review("Vlad N.", 4.3, "Spațiu generos, organizare bună. Parcarea e aglomerată.", new DateOnly(2025, 10, 5))
        ];
        repo.Add(e1);

        // ── 2. Sala Palatului ──
        var e2 = (EventVenue)factory.CreateResource("Sala Palatului", 4000, 2500m);
        e2.Description = "Sala emblematică a Bucureștiului, cu acustică excepțională. Găzduiește concerte, spectacole și evenimente culturale de top.";
        e2.Category = EventCategory.Concert;
        e2.Location = new Location("București", "Str. Ion Câmpineanu 28", "România");
        e2.EventDate = new DateOnly(2026, 7, 20);
        e2.StartTime = new TimeOnly(19, 0);
        e2.EndTime = new TimeOnly(22, 30);
        e2.Organizer = "Events International";
        e2.Duration = "3.5 ore";
        e2.AgeRestriction = "12+";
        e2.Images = ["https://images.unsplash.com/photo-1459749411175-04bf5292ceea?w=600"];
        e2.Facilities = EventFacility.Parking | EventFacility.Bar | EventFacility.VIPArea | EventFacility.SeatsAvailable;
        e2.Reviews =
        [
            new Review("Ana V.", 4.8, "Acustică incredibilă! Experiență de neuitat.", new DateOnly(2025, 11, 12)),
            new Review("Cristian M.", 4.6, "Locurile VIP merită investiția.", new DateOnly(2025, 12, 1))
        ];
        repo.Add(e2);

        // ── 3. Cluj Arena ──
        var e3 = (EventVenue)factory.CreateResource("Cluj Arena", 25000, 8000m);
        e3.Description = "Stadionul multifuncțional din Cluj-Napoca, perfect pentru concerte mari, festivaluri și evenimente sportive.";
        e3.Category = EventCategory.Festival;
        e3.Location = new Location("Cluj-Napoca", "Aleea Stadionului 2", "România");
        e3.EventDate = new DateOnly(2026, 8, 5);
        e3.StartTime = new TimeOnly(16, 0);
        e3.EndTime = new TimeOnly(23, 59);
        e3.Organizer = "Untold Festival";
        e3.Duration = "4 zile";
        e3.AgeRestriction = "16+";
        e3.Images = ["https://images.unsplash.com/photo-1493225457124-a3eb161ffa5f?w=600"];
        e3.Facilities = EventFacility.Parking | EventFacility.Food | EventFacility.Bar | EventFacility.VIPArea | EventFacility.SeatsAvailable;
        e3.Reviews =
        [
            new Review("Diana P.", 5.0, "Untold este cel mai bun festival din Europa!", new DateOnly(2025, 8, 10)),
            new Review("Martin K.", 4.7, "Amazing atmosphere, well organized.", new DateOnly(2025, 8, 12))
        ];
        repo.Add(e3);

        // ── 4. Palace of Culture Iasi ──
        var e4 = (EventVenue)factory.CreateResource("Palatul Culturii Iași", 800, 900m);
        e4.Description = "Monument istoric și centru cultural, ideal pentru conferințe, spectacole de teatru și evenimente academice.";
        e4.Category = EventCategory.Conference;
        e4.Location = new Location("Iași", "Bd. Ștefan cel Mare și Sfânt 1", "România");
        e4.EventDate = new DateOnly(2026, 10, 10);
        e4.StartTime = new TimeOnly(10, 0);
        e4.EndTime = new TimeOnly(17, 0);
        e4.Organizer = "Primăria Iași";
        e4.Duration = "2 zile";
        e4.AgeRestriction = null;
        e4.Images = ["https://images.unsplash.com/photo-1505373877841-8d25f7d46678?w=600"];
        e4.Facilities = EventFacility.Parking | EventFacility.Food | EventFacility.SeatsAvailable;
        e4.Reviews =
        [
            new Review("Gabriela I.", 4.5, "Atmosferă specială, clădirea este impresionantă.", new DateOnly(2025, 10, 20))
        ];
        repo.Add(e4);
    }
}
