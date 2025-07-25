//using Moq;
//using System.Linq.Expressions;
//using SIGEBI.Domain.Entities.circulation;
//using SIGEBI.Persistence.Context;
//using SIGEBI.Persistence.Repositories;
//using SIGEBI.Application.Contracts;

//namespace SIGEBI.Persistence.Test
//{
//    [TestClass]
//    public class ReservationRepositoryTests
//    {
//        private ReservationRepository _repository = null!;
//        private Mock<SIGEBIContext> _mockContext = null!; // Mock para el contexto
//        private Mock<IAppLogger<ReservationRepository>> _mockLogger = null!;

//        [TestInitialize]
//        public void Setup()
//        {
//            _mockContext = new Mock<SIGEBIContext>();
//            _mockLogger = new Mock<IAppLogger<ReservationRepository>>();
//            _repository = new ReservationRepository(_mockContext.Object, _mockLogger.Object);
//        }

//        [TestMethod]
//        public async Task GetByIdAsync_ThrowsNotImplementedException()
//        {
//            await Assert.ThrowsExceptionAsync<NotImplementedException>(() => _repository.GetByIdAsync(1));
//        }

//        [TestMethod]
//        public async Task GetAllAsync_ThrowsNotImplementedException()
//        {
//            Expression<Func<Reservation, bool>> filter = r => r.Id > 0;
//            await Assert.ThrowsExceptionAsync<NotImplementedException>(() => _repository.GetAllAsync(filter));
//        }

//        [TestMethod]
//        public async Task ExistsAsync_ThrowsNotImplementedException()
//        {
//            Expression<Func<Reservation, bool>> filter = r => r.Id > 0;
//            await Assert.ThrowsExceptionAsync<NotImplementedException>(() => _repository.ExistsAsync(filter));
//        }

//        [TestMethod]
//        public async Task AddAsync_ThrowsNotImplementedException()
//        {
//            var reservation = new Reservation
//            {
//                CreatedBy = "TestUser",
//                UpdatedBy = "TestUser",
//                DeletedBy = "TestUser"
//            };
//            await Assert.ThrowsExceptionAsync<NotImplementedException>(() => _repository.AddAsync(reservation));
//        }

//        [TestMethod]
//        public async Task UpdateAsync_ThrowsNotImplementedException()
//        {
//            var reservation = new Reservation
//            {
//                CreatedBy = "TestUser",
//                UpdatedBy = "TestUser",
//                DeletedBy = "TestUser"
//            };
//            await Assert.ThrowsExceptionAsync<NotImplementedException>(() => _repository.UpdateAsync(reservation));
//        }

//        [TestMethod]
//        public async Task DisableAsync_ThrowsNotImplementedException()
//        {
//            var reservation = new Reservation
//            {
//                Id = 1, // Assuming Id is required for DisableAsync
//                CreatedBy = "TestUser",
//                UpdatedBy = "TestUser",
//                DeletedBy = "TestUser"
//            };
//            await Assert.ThrowsExceptionAsync<NotImplementedException>(() => _repository.DisableAsync(reservation.Id, reservation.DeletedBy));
//        }
//    }
//}
