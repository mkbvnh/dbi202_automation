create proc Display_EmployeesDepartment @dept_Id int
as
begin
	select DEPARTMENT.NAME as Department_Name, count(EMPLOYEE.EMP_ID) as NumberOfEmployees 
	from DEPARTMENT left join EMPLOYEE
	on DEPARTMENT.DEPT_ID = EMPLOYEE.DEPT_ID
	where DEPARTMENT.DEPT_ID = @dept_Id
	group by DEPARTMENT.NAME
end
