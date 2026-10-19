namespace BD2;

public class Bai2
{
    static void Main1(string[] args)
    {
        Console.OutputEncoding = System.Text.Encoding.UTF8;
        // Khởi tạo mảng có dung lượng cấp phát là 10
        int[] data = new int[10];
        data[0] = 4;
        data[1] = 7;
        data[2] = -2;
        data[3] = 9;
        data[4] = 50;
        int count = 5;

        Console.WriteLine("Mảng ban đầu");
        PrintArray(data, count);

        // Ca 1: Chèn 99 tại index 2
        Console.WriteLine("\nCa 1: Chèn 99 tại index 2");
        Insert(data, ref count, 2, 99);
        PrintArray(data, count);

        // Ca 2: Xóa phần tử tại index 1
        Console.WriteLine("\nCa 2: Xóa phần tử tại index 1");
        RemoveAt(data, ref count, 1);
        PrintArray(data, count);

        // Ca 3: Chèn tại đầu mảng (index 0)
        Console.WriteLine("\nCa 3: Chèn 100 tại đầu mảng (index 0)");
        Insert(data, ref count, 0, 100);
        PrintArray(data, count);

        // Ca 4: Chèn tại cuối mảng (index = count)
        Console.WriteLine("\nCa 4: Chèn 200 tại cuối mảng (index = count)");
        Insert(data, ref count, count, 200);
        PrintArray(data, count);

        // Ca 5: Xóa tại đầu mảng (index 0)
        Console.WriteLine("\nCa 5: Xóa tại đầu mảng (index 0)");
        RemoveAt(data, ref count, 0);
        PrintArray(data, count);

        // Ca 6: Xóa tại cuối mảng (index = count - 1)
        Console.WriteLine("\nCa 6: Xóa tại cuối mảng (index = count - 1)");
        RemoveAt(data, ref count, count - 1);
        PrintArray(data, count);

        // Ca 7: Kiểm thử ngoại lệ - Index âm
        Console.WriteLine("\nCa 7: Chèn với index âm (-1)");
        try
        {
            Insert(data, ref count, -1, 50);
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Ngoại lệ: {ex.GetType().Name} - {ex.Message}");
        }

        // Ca 8: Kiểm thử ngoại lệ - Xóa tại index = count
        Console.WriteLine("\nCa 8: Xóa tại index = count");
        try
        {
            RemoveAt(data, ref count, count);
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Ngoại lệ: {ex.GetType().Name} - {ex.Message}");
        }

        // Ca 9: Kiểm thử ngoại lệ - Mảng đã đầy
        Console.WriteLine("\nCa 9: Chèn khi mảng đã đầy");
        try
        {
            int[] fullArr = new int[3] { 10, 20, 30 };
            int fullCount = 3;
            Insert(fullArr, ref fullCount, 1, 999);
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Ngoại lệ: {ex.GetType().Name} - {ex.Message}");
        }
    }
    static void Insert(int[] data, ref int count, int index, int value)
    {
        // Kiểm tra điều kiện dữ liệu có null không
        if (data == null)
        {
            throw new ArgumentNullException(nameof(data));
        }
        // Kiểm tra điều kiện mảng đầy dung lượng cấp phát
        if (count >= data.Length)
        {
            // Phát sinh ngoại lệ InvalidOperationException khi mảng hết chỗ trống
            throw new InvalidOperationException("Mảng đã đầy, không thể chèn thêm phần tử mới");
        }

        // Kiểm tra chỉ số chèn có nằm trong đoạn hợp lệ [0, count]
        if (index < 0 || index > count)
        {
            // Phát sinh ngoại lệ ArgumentOutOfRangeException khi chỉ số không hợp lệ
            throw new ArgumentOutOfRangeException(nameof(index), "Chỉ số chèn nằm ngoài phạm vi hợp lệ [0, count]");
        }

        // Dịch chuyển các phần tử từ chỉ số count - 1 lùi về index sang phải 1 vị trí
        for (int i = count - 1; i >= index; i--)
        {
            data[i + 1] = data[i];
        }

        // Đưa giá trị cần chèn vào vị trí index đã được làm trống
        data[index] = value;

        // Tăng số lượng phần tử thực tế hợp lệ lên 1
        count++;
    }

    static void RemoveAt(int[] data, ref int count, int index)
    {
        // Kiểm tra chỉ số xóa có nằm trong đoạn hợp lệ [0, count - 1]
        if (index < 0 || index >= count)
        {
            // Phát sinh ngoại lệ ArgumentOutOfRangeException khi chỉ số vượt phạm vi
            throw new ArgumentOutOfRangeException(nameof(index), "Chỉ số xóa nằm ngoài phạm vi hợp lệ [0, count - 1]");
        }

        // Dịch chuyển các phần tử từ index + 1 đến count - 1 sang trái 1 vị trí
        for (int i = index; i < count - 1; i++)
        {
            // Ghi đè phần tử phía sau lên vị trí phía trước
            data[i] = data[i + 1];
        }

        // Giảm số lượng phần tử thực tế hợp lệ đi 1
        count--;
    }

    // Hàm tiện ích hỗ trợ xuất danh sách mảng ra màn hình console
    static void PrintArray(int[] data, int count)
    {
        Console.Write("[");
        for (int i = 0; i < count; i++)
        {
            Console.Write(data[i] + (i < count - 1 ? ", " : ""));
        }

        Console.WriteLine($"] (count = {count}, Capacity = {data.Length})");
    }
}
