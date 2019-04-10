select T.*, count(EMP_ID) as NumberOfEmployees
from (select DEPT_ID, BRANCH_ID
from DEPARTMENT , BRANCH) as T left join EMPLOYEE 
on T.DEPT_ID = EMPLOYEE.DEPT_ID and T.BRANCH_ID = ASSIGNED_BRANCH_ID
group by T.DEPT_ID, T.BRANCH_ID
