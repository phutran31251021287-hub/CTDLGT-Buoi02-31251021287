using System;

namespace BD2;

public class Bai4
{
    static void Main1(string[] args)
    {
        Console.OutputEncoding = System.Text.Encoding.UTF8;
        // Khởi tạo mảng động với Capacity mặc định = 4
        DynamicArray<int> list = new DynamicArray<int>();

        Console.WriteLine("\n1. Thêm lần lượt từ 1 đến 10 và theo dõi Count, Capacity");
        for (int i = 1; i <= 10; i++)
        {
            int oldCapacity = list.Capacity;
            list.Add(i);

            // Kiểm tra sự thay đổi dung lượng để ghi nhận số phần tử sao chép
            if (list.Capacity != oldCapacity)
            {
                Console.WriteLine(
                    $"  [Mở rộng] Đã sao chép {list.Count - 1} phần tử cũ. Capacity: {oldCapacity} -> {list.Capacity}");
            }

            Console.WriteLine($"Thêm {i,2}: Count = {list.Count}, Capacity = {list.Capacity}");
        }

        Console.WriteLine("\n2. Đọc và cập nhật phần tử qua Indexer");
        Console.WriteLine($"Giá trị tại index 3 ban đầu: {list[3]}");
        list[3] = 99;
        Console.WriteLine($"Giá trị tại index 3 sau khi gán list[3] = 99: {list[3]}");

        Console.WriteLine("\n3. Kiểm tra thứ tự dữ liệu giữ nguyên sau mở rộng");
        Console.Write("Danh sách hiện tại: ");
        for (int i = 0; i < list.Count; i++)
        {
            Console.Write($"{list[i]} ");
        }

        Console.WriteLine();

        Console.WriteLine("\n4. Kiểm thử các trường hợp chỉ số không hợp lệ");
        // Kiểm thử chỉ số âm
        try
        {
            Console.WriteLine("Thử đọc với index = -1...");
            int val = list[-1];
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Bắt ngoại lệ: {ex.GetType().Name} - {ex.Message}");
        }

        // Kiểm thử chỉ số bằng Count
        try
        {
            Console.WriteLine($"Thử đọc với index = Count ({list.Count})...");
            int val = list[list.Count];
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Bắt ngoại lệ: {ex.GetType().Name} - {ex.Message}");
        }

        // Kiểm thử khởi tạo capacity âm
        try
        {
            Console.WriteLine("Thử khởi tạo DynamicArray với capacity = -5...");
            DynamicArray<int> invalidArr = new DynamicArray<int>(-5);
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Bắt ngoại lệ: {ex.GetType().Name} - {ex.Message}");
        }
    }
}

// Lớp mảng động tự định nghĩa mô phỏng cơ chế của List<T>
public class DynamicArray<T>
{
    // Mảng tĩnh nội bộ dùng làm vùng lưu trữ dữ liệu thực tế
    private T[] items;

    // Số phần tử hợp lệ đang có trong mảng
    public int Count { get; private set; }

    // Dung lượng tối đa của mảng nội bộ hiện tại
    public int Capacity => items.Length;

    // Phương thức khởi tạo với dung lượng mặc định bằng 4
    public DynamicArray(int capacity = 4)
    {
        // Từ chối dung lượng khởi tạo âm
        if (capacity < 0)
        {
            throw new ArgumentOutOfRangeException(nameof(capacity), "Dung lượng khởi tạo không được là số âm");
        }

        // Cấp phát bộ nhớ mảng tĩnh với dung lượng được chỉ định
        items = new T[capacity];
        Count = 0;
    }

    // Phương thức kiểm tra và mở rộng mảng khi đã hết chỗ trống
    private void EnsureCapacity()
    {
        // Chỉ nhân đôi dung lượng khi số phần tử thực tế đã chạm ngưỡng Capacity
        if (Count == Capacity)
        {
            // Tính dung lượng mới gấp đôi dung lượng cũ (nếu Capacity = 0 thì mặc định là 4)
            int newCapacity = Capacity == 0 ? 4 : Capacity * 2;

            // Cấp phát mảng tạm thời với dung lượng mới
            T[] newItems = new T[newCapacity];

            // Sao chép chính xác Count phần tử từ mảng cũ sang mảng mới
            for (int i = 0; i < Count; i++)
            {
                newItems[i] = items[i];
            }

            // Chuyển tham chiếu items sang mảng mới đã mở rộng
            items = newItems;
        }
    }

    // Phương thức thêm phần tử vào cuối mảng động
    public void Add(T item)
    {
        // Kiểm tra và thực hiện tăng dung lượng nếu mảng đã đầy
        EnsureCapacity();

        // Đưa phần tử mới vào ô trống đầu tiên ở cuối mảng
        items[Count] = item;

        // Cập nhật số phần tử thực tế tăng thêm 1
        Count++;
    }

    // Indexer cho phép truy cập và cập nhật phần tử theo cú pháp array[index]
    public T this[int index]
    {
        get
        {
            // Ràng buộc chỉ số truy cập nằm trong đoạn [0, Count - 1]
            if (index < 0 || index >= Count)
            {
                throw new ArgumentOutOfRangeException(nameof(index), "Chỉ số truy cập nằm ngoài phạm vi danh sách");
            }
            return items[index];
        }
        set
        {
            // Ràng buộc chỉ số cập nhật nằm trong đoạn [0, Count - 1]
            if (index < 0 || index >= Count)
            {
                throw new ArgumentOutOfRangeException(nameof(index), "Chỉ số cập nhật nằm ngoài phạm vi danh sách");
            }
            items[index] = value;
        }
    }
}