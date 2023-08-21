using TrybeHotel.Models;
using TrybeHotel.Dto;

namespace TrybeHotel.Repository
{
    public class HotelRepository : IHotelRepository
    {
        protected readonly ITrybeHotelContext _context;
        public HotelRepository(ITrybeHotelContext context)
        {
            _context = context;
        }

        public IEnumerable<HotelDto> GetHotels()
        {
             return _context.Hotels.Select(h => new HotelDto
            {
                HotelId = h.HotelId,
                Name = h.Name,
                Address = h.Address,
                CityId = h.CityId,
                CityName = h.City!.Name,
            }).ToList();
        }
        
        public HotelDto AddHotel(Hotel hotel)
        {
            _context.Hotels.Add(hotel);
            _context.SaveChanges();
            var hotels = _context.Hotels;
            var actualHotel = from h in hotels
                            where h.Name == hotel.Name
                            select new HotelDto
                            {
                                HotelId = h.HotelId,
                                Name = h.Name,
                                Address = h.Address,
                                CityId = h.CityId,
                                CityName = h.City!.Name,
                            };
            return actualHotel.First();
        }
    }
}