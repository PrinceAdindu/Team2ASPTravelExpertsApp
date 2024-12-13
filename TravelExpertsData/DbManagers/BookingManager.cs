using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TravelExpertsData.DbManagers
{
    public class BookingManager
    {
        private TravelExpertsContext _context { get; set; }
        public BookingManager(TravelExpertsContext context)
        {
            this._context = context;
        }
        public async Task AddBookingAsync(Booking booking)
        {
            try
            {
                _context.Bookings.AddAsync(booking);
                await _context.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                throw new InvalidOperationException("An error occured while creating the booking.", ex);
            }

        }

        public async Task AddBookingDetailsAsync(BookingDetail bookingDetail)
        {
            try
            {
                _context.BookingDetails.AddAsync(bookingDetail);
                await _context.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                throw new InvalidOperationException("An error occured while adding the booking details.", ex);
            }

        }

        public BookingDetail? GetBookingDetail(int bookingId) { 
            var bookingDetail = _context.BookingDetails.FirstOrDefault(b=>b.BookingId == bookingId);
            if (bookingDetail != null) return bookingDetail;
            else return null;
        }

        public static List<BookingsDTO> GetBookingSummary(int customerId)
        {
            // calling db context
            using (TravelExpertsContext db = new TravelExpertsContext())
            {
                List<BookingsDTO> bookingSummary =      /*joining 6 different tables*/

                    /*Tables that are being joined:     */
                    db.Bookings.Join(                   /*Bookings*/
                        db.BookingDetails,              /*BookingDetails*/
                        b => b.BookingId,
                        bd => bd.BookingDetailId,
                        (b, bd) => new { b, bd }).Join(
                        db.TripTypes,                   /*TripTypes*/
                        bb => bb.b.TripTypeId,
                        tt => tt.TripTypeId,
                        (bb, tt) => new { bb, tt }).Join(
                        db.Classes,                     /*Classes*/
                        bbd => bbd.bb.bd.ClassId,
                        c => c.ClassId,
                        (bbd, c) => new { bbd, c }).Join(
                        db.Regions,                     /*Regions*/
                        bbbd => bbbd.bbd.bb.bd.RegionId,
                        r => r.RegionId,
                        (bbbd, r) => new { bbbd, r }).Join(
                        db.Fees,                        /*Fees*/
                        d => d.bbbd.bbd.bb.bd.FeeId,
                        f => f.FeeId,
                        (d, f) => new { d, f })


                        .Where(
                         cust => cust.d.bbbd.bbd.bb.b.CustomerId == customerId)   /*where clause to specify which customer*/
                        .Select(                                                /*select clause to fetch specific columns*/
                        book => new BookingsDTO
                        {
                            BookingDescription = book.d.bbbd.bbd.bb.bd.Description,
                            BookingDestination = book.d.bbbd.bbd.bb.bd.Destination,
                            BookingNo = book.d.bbbd.bbd.bb.b.BookingNo,
                            TripTypeName = book.d.bbbd.bbd.tt.Ttname,
                            ClassName = book.d.bbbd.c.ClassName,
                            RegionName = book.d.r.RegionName,
                            FeeName = book.f.FeeName,
                            FeeAmount = book.f.FeeAmt,
                            BasePrice = book.d.bbbd.bbd.bb.bd.BasePrice,
                            AgencyCommission = book.d.bbbd.bbd.bb.bd.AgencyCommission

                        }).ToList();
                return bookingSummary;
            }
        }
    }
}
