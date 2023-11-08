namespace WebSite.Extension
{
    public static class File
    {
        public static async Task WriteWithProgressAsync(string path, byte[] data, Action<double> action)
        {
            int blockSize = 1024; // Размер блока для записи за один раз
            int totalWritten = 0; // Общее количество записанных байтов

            try
            {
                // Получение текущей директории проекта
                string projectDirectory = AppDomain.CurrentDomain.BaseDirectory;

                // Создание полного пути к файлу
                string fullPath = Path.Combine(projectDirectory, path);

                // Создание всех директорий в пути, если они еще не существуют
                Directory.CreateDirectory(Path.GetDirectoryName(fullPath));

                using (FileStream stream = new FileStream(fullPath, FileMode.Create))
                {
                    for (int i = 0; i < data.Length; i += blockSize)
                    {
                        int toWrite = Math.Min(blockSize, data.Length - i);
                        await stream.WriteAsync(data, i, toWrite);

                        totalWritten += toWrite;

                        // Обновление прогресса
                        double progress = (double)totalWritten / data.Length * 100;
                        action?.Invoke(progress);
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Ошибка: {ex.Message}");
            }
        }

    }
}
