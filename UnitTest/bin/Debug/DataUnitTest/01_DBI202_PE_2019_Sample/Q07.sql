create trigger Trigger_Delete_Employee
On Employee
instead of delete
as
begin
	select EMP_ID, FIRST_NAME, LAST_NAME, DEPARTMENT.NAME as DEPARTMENT_NAME
	from deleted, DEPARTMENT
	where deleted.DEPT_ID = DEPARTMENT.DEPT_ID
end
