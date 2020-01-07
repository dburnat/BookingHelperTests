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
        private Housekeeper _houseKeeper;
        private Mock<IUnitOfWork> _unitOfWork;
        private Mock<IStatementGenerator> _statementGenerator;
        private Mock<IEmailSender> _emailSender;
        private Mock<IXtraMessageBox> _messageBox;
        [SetUp]
        public void Setup()
        {

            _houseKeeper = new Housekeeper();
            _unitOfWork = new Mock<IUnitOfWork>();
            _statementGenerator = new Mock<IStatementGenerator>();
            _emailSender = new Mock<IEmailSender>();
            _messageBox = new Mock<IXtraMessageBox>();
            _housekeeperService = new HousekeeperService(
                _unitOfWork.Object, _statementGenerator.Object, _emailSender.Object
                , _messageBox.Object);
        }
        [Test]
        public void SendStatementsEmails_HouseKeepersEmailIsNull_ShouldNotGenerateStatement()
        {
            var date = DateTime.Now;
            _housekeeperService.SendStatementEmails(date);
            _houseKeeper.Email = null;

            _statementGenerator.Verify(c => c.SaveStatement(
                _houseKeeper.Oid, _houseKeeper.FullName, date), Times.Never);
        }

        [Test]
        public void SendStatementEmails__ShouldReturnTrue()
        {
            var date = DateTime.Now;
            _houseKeeper.Email = "adam@example.com";
             
            _housekeeperService.SendStatementEmails(date);

            Assert.AreEqual(true,_housekeeperService.SendStatementEmails(date));
        }

        [Test]
        public void SendStatementEmails_HouseKeeperHasNoFullName_ShouldNotGenerateStatement()
        {
            var date = DateTime.Now;
            _houseKeeper.FullName = "";

            _housekeeperService.SendStatementEmails(date);
            
            _statementGenerator.Verify(c => c.SaveStatement
                (_houseKeeper.Oid, _houseKeeper.FullName, date), Times.Never);
        }

        [Test]
        public void SendStatementEmails_EverythingIsCorrect_ShouldReturnTrue()
        {
            var date = DateTime.Now;

            Assert.AreEqual(true,_housekeeperService.SendStatementEmails(date));
        }
        
    }
}
