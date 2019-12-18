using BookingHelper;
using Moq;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Text;

namespace HouseKeeperHelperTests
{
    public class HouseKeeperServiceTests
    {
        private HousekeeperService _housekeeperService;
        [SetUp]
        public void Setup()
        {
            var _unitOfWork = new Mock<IUnitOfWork>();
            var _statementGenerator = new Mock<IStatementGenerator>();
            var _emailSender = new Mock<IEmailSender>();
            var _messageBox = new Mock<IXtraMessageBox>();

            _housekeeperService = new HousekeeperService(
                _unitOfWork.Object, _statementGenerator.Object, _emailSender.Object
                , _messageBox.Object);
        }

        [Test]
        public void Test1()
        {
            var date = new DateTime(2019, 12, 18);
            _housekeeperService.SendStatementEmails(date);

            
        }


    }
}
