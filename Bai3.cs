namespace BD2;

public class Student
{
    public string Id { get; set; } = "";
    public string FullName { get; set; } = "";
    public double Score { get; set; }
}

public class Bai3
{
    static void Main1(string[] args)
    {
        Console.OutputEncoding = System.Text.Encoding.UTF8;
        // Chuẩn bị 5 sinh viên ban đầu để kiểm thử
        AddStudent("SV01", "Nguyen Van A", 7.5);
        AddStudent("SV02", "Le Thi B", 4.0);
        AddStudent("SV03", "Tran Van C", 8.0);
        AddStudent("SV04", "Pham Thi D", 9.5);
        AddStudent("SV05", "Hoang Van E", 3.5);

        // Chạy các ca kiểm thử cứng thay vì Menu để dễ quan sát output
        Console.WriteLine("\nKiểm thử");
        AddStudent("SV01", "Test Trung", 5.0); // Từ chối trùng
        AddStudent("SV06", "Sai Diem", 11);    // Từ chối điểm sai
        UpdateStudent("SV02", "Le Thi B Update", 8.5); // Cập nhật
        SearchByName("thi"); // Tìm có kết quả
        SearchByName("XYZ"); // Tìm không kết quả
        RemoveStudent("SV03"); // Xóa thành công
        RemoveStudent("SV99"); // Xóa mã không tồn tại
        PrintAndStatistics(); // In thống kê
    }
    // Danh sách lưu trữ sinh viên hiện tại trong bộ nhớ
    static List<Student> students = new List<Student>();

    public static void AddStudent(string id, string name, double score)
    {
        // Duyệt tuần tự O(n) để đảm bảo mã sinh viên là duy nhất
        foreach (Student s in students)
        {
            if (s.Id == id)
            {
                Console.WriteLine("Lỗi: Mã sinh viên đã tồn tại");
                return;
            }
        }

        // Từ chối dữ liệu rác bằng cách giới hạn miền giá trị điểm
        if (score < 0 || score > 10)
        {
            Console.WriteLine("Lỗi: Điểm phải nằm trong đoạn [0, 10]");
            return;
        }

        // Chèn sinh viên vào cuối mảng động nội bộ
        students.Add(new Student { Id = id, FullName = name, Score = score });
        Console.WriteLine("Đã thêm");
        Console.WriteLine($"[{id}] {name} - {score}");
    }

    public static void UpdateStudent(string id, string newName, double newScore)
    {
        // Kiểm tra điểm hợp lệ trước khi sửa đổi dữ liệu thực tế
        if (newScore < 0 || newScore > 10)
        {
            Console.WriteLine("Lỗi: Điểm mới không hợp lệ");
            return;
        }

        // Duyệt tìm đối tượng trực tiếp theo tham chiếu để cập nhật
        for (int i = 0; i < students.Count; i++)
        {
            if (students[i].Id == id)
            {
                students[i].FullName = newName;
                students[i].Score = newScore;
                Console.WriteLine("Cập nhật thành công");
                return;
            }
        }
        Console.WriteLine("Lỗi: Không tìm thấy mã sinh viên");
    }

    public static void RemoveStudent(string id)
    {
        for (int i = 0; i < students.Count; i++)
        {
            if (students[i].Id == id)
            {
                // Gọi RemoveAt sẽ buộc List dịch n - i - 1 phần tử phía sau lên
                students.RemoveAt(i);
                Console.WriteLine("Đã xóa");
                return;
            }
        }
        Console.WriteLine("Lỗi: Không tìm thấy mã sinh viên để xóa");
    }

    public static void SearchByName(string namePart)
    {
        bool found = false;
        // Duyệt toàn bộ mảng và so sánh chuỗi không phân biệt hoa thường
        foreach (Student s in students)
        {
            if (s.FullName.Contains(namePart, StringComparison.OrdinalIgnoreCase))
            {
                Console.WriteLine($"[{s.Id}] {s.FullName} - {s.Score}");
                found = true;
            }
        }
        if (!found) Console.WriteLine("Không tìm thấy kết quả");
    }

    public static void PrintAndStatistics()
    {
        // Xử lý ca biên: danh sách rỗng để tránh chia cho 0 khi tính trung bình
        if (students.Count == 0)
        {
            Console.WriteLine("Danh sách rỗng");
            return;
        }

        double sum = 0;
        int countPass = 0;

        Console.WriteLine("\nDanh sách sinh viên");
        foreach (Student s in students)
        {
            Console.WriteLine($"[{s.Id}] {s.FullName} - Điểm: {s.Score}");
            sum += s.Score;
            // Đếm số sinh viên đạt yêu cầu
            if (s.Score >= 5) countPass++;
        }

        // Ép kiểu (double) tự động nội ngầm khi sum đã là double
        double average = sum / students.Count;
        Console.WriteLine($"\nĐiểm trung bình: {average:F2}");
        Console.WriteLine($"Số sinh viên đạt (>=5): {countPass}");
    }
}