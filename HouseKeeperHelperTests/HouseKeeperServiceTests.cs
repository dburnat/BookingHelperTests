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
        private HouseKeeper _houseKeeper;
        private Mock<IUnitOfWork> _unitOfWork;
        private Mock<IStatementGenerator> _statementGenerator;
        private Mock<IEmailSender> _emailSender;
        private Mock<IXtraMessageBox> _messageBox;
        [SetUp]
        public void Setup()
        {

            _house
            _unitOfWork = new Mock<IUnitOfWork>();
            _statementGenerator = new Mock<IStatementGenerator>();
            _emailSender = new Mock<IEmailSender>();
            _messageBox = new Mock<IXtraMessageBox>();

            _housekeeperService = new HousekeeperService(
                _unitOfWork.Object, _statementGenerator.Object, _emailSender.Object
                , _messageBox.Object);
        }

        [Test]
        public void Test1()
        {
            _housekeeperService.SendStatementEmails(date);

            _statementGenerator.Verify(d => d.SaveStatement());
        }



        [Test]
        public void SendStatementsEmails_HouseKeepersEmailIsNull_ShouldNotGenerateStatement()
        {
            _houseKeeper.email = null;
            _housekeeperService.SendStatementEmails(date);

            _statementGenerator.Verify(c => c.SaveStatement(
                _houseKeeper.Oid, _houseKeeper.FullName, date), Times.Never);
        }

    }
}
