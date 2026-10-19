namespace BD2;

public class Bai1
{
    static void Main(string[] args)
    {
        int n = Convert.ToInt32(Console.ReadLine());
        
        long[] arr = new long[n];
        
        string inputArray = Console.ReadLine();
        
        // Tách chuỗi thành mảng các chuỗi con, loại bỏ khoảng trắng thừa
        string[] text = inputArray.Split(new string[] { " " }, StringSplitOptions.RemoveEmptyEntries);
        
        // Chuyển đổi từng chuỗi sang số nguyên long và lưu vào mảng
        for (int i = 0; i < n; i++)
        {
            arr[i] = long.Parse(text[i]);
        }

        // Khởi tạo min và max bằng giá trị của phần tử đầu tiên arr[0]
        long min = arr[0];
        long max = arr[0];
        long sum = 0;          // Biến tổng kiểu long để tránh tràn số khi cộng dồn
        int countNegative = 0; // Biến đếm số phần tử âm
        int countPositive = 0; // Biến đếm số phần tử dương
        int countZero = 0;     // Biến đếm số phần tử bằng 0

        // Duyệt mảng để gom tất cả thao tác thống kê
        foreach (long val in arr)
        {
            sum += val; // Cộng dồn giá trị vào tổng

            if (val < min) min = val; // Cập nhật giá trị nhỏ nhất
            if (val > max) max = val; // Cập nhật giá trị lớn nhất

            // Phân loại số âm, dương và số 0
            if (val < 0) countNegative++;
            else if (val > 0) countPositive++;
            else countZero++;
        }

        // Ép kiểu sum sang double trước phép chia để giữ phần thập phân
        double average = (double)sum / arr.Length;

        // In kết quả thống kê
        Console.WriteLine($"Tong: {sum}");
        Console.WriteLine($"Min: {min}");
        Console.WriteLine($"Max: {max}");
        Console.WriteLine($"Trung binh: {average:F2}");
        Console.WriteLine($"So phan tu am: {countNegative}");
        Console.WriteLine($"So phan tu duong: {countPositive}");
        Console.WriteLine($"So phan tu 0: {countZero}");

        // Đảo mảng tại chỗ
        int left = 0;              // Chỉ số bắt đầu từ đầu mảng
        int right = arr.Length - 1; // Chỉ số bắt đầu từ cuối mảng

        // Tiến hành hoán đổi cho đến khi 2 con trỏ gặp hoặc vượt qua nhau
        while (left < right)
        {
            // Hoán đổi giá trị giữa arr[left] và arr[right] qua biến gian temp
            long temp = arr[left];
            arr[left] = arr[right];
            arr[right] = temp;

            left++;  // Di chuyển con trỏ trái sang phải
            right--; // Di chuyển con trỏ phải sang trái
        }

        // In mảng sau khi đảo
        foreach (long val in arr)
        {
            Console.Write($"{val} ");
        }
        Console.WriteLine();
    }
}