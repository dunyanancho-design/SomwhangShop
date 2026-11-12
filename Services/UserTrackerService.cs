using Microsoft.AspNetCore.Components.Server.Circuits;

namespace SomwhangShop.Services
{
    // ตัวเก็บยอดคนออนไลน์ (Singleton: ใช้ร่วมกันทั้งเซิร์ฟเวอร์)
    public class UserTrackerService
    {
        private int _userCount = 0;

        public int GetCount() => _userCount;

        public void UserConnected(string connectionId)
        {
            Interlocked.Increment(ref _userCount);
            LogStatus(connectionId, "เข้า");
        }

        public void UserDisconnected(string connectionId)
        {
            Interlocked.Decrement(ref _userCount);
            LogStatus(connectionId, "ออก");
        }

        private void LogStatus(string id, string action)
        {
            // ถ้าเหลือ 1 คน (คือเราเอง) -> สีเขียว (ปลอดภัย)
            // ถ้ามากกว่า 1 คน (มีลูกค้า) -> สีแดง (อันตราย อย่าพึ่งรีรัน!)
            bool isSafeToRestart = _userCount <= 1;

            var color = isSafeToRestart ? ConsoleColor.Green : ConsoleColor.Red;
            var msg = isSafeToRestart ? "✅ ทางสะดวก (น่าจะมีแค่เรา)" : "⚠️ มีลูกค้าใช้งานอยู่!";

            Console.ForegroundColor = color;
            Console.WriteLine($"[{DateTime.Now:HH:mm:ss}] 👥 Monitor: คน{action}เว็บ (ID: {id.Substring(0, 5)}..) | รวมออนไลน์: {_userCount} คน | {msg}");
            Console.ResetColor();
        }
    }

    // ตัวดักจับตอนคนเปิด/ปิดหน้าเว็บ (Scoped: แยกตามคน)
    public class TrackingCircuitHandler : CircuitHandler
    {
        private readonly UserTrackerService _tracker;
        public TrackingCircuitHandler(UserTrackerService tracker) => _tracker = tracker;

        public override Task OnCircuitOpenedAsync(Circuit circuit, CancellationToken cancellationToken)
        {
            _tracker.UserConnected(circuit.Id);
            return base.OnCircuitOpenedAsync(circuit, cancellationToken);
        }

        public override Task OnCircuitClosedAsync(Circuit circuit, CancellationToken cancellationToken)
        {
            _tracker.UserDisconnected(circuit.Id);
            return base.OnCircuitClosedAsync(circuit, cancellationToken);
        }
    }
}