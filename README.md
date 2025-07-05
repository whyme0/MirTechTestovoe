## Запуск проекта
Для `Visual Studio` необходимо:
1. Открыть решение `MirTechTestovoe.sln`
2. Инициализировать базу данных. Откройте `Package Manager Console` и выполните команду:
   ```
   Update-Database
   ```
3. Запустить проект, нажав `F5`

## Приложение к проекту: T-SQL скрипты

1. Выборка всех сотрудников с зарплатой > 10000
```tsql
SELECT [Id]
	[Department],
	[FullName],
	[BirthDate],
	[EmploymentDate],
	[Salary]
FROM [DbName].[dbo].[Employees]
WHERE [Salary] > 10000
```
2. Удаление сотрудников возраста более 70 лет
```tsql
DELETE FROM [DbName].[dbo].[Employees]
WHERE DATEDIFF(YEAR, [BirthDate], GETDATE()) > 70;
```
3. Обновление зарплаты до 15000 тем, у кого она меньше этого значения
```tsql
UPDATE [DbName].[dbo].[Employees]
SET [Salary] = 15000
WHERE [Salary] < 15000;
```