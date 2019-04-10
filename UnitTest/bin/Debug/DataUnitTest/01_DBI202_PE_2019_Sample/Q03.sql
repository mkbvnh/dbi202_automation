select EMP_ID, FIRST_NAME, LAST_NAME, DEPT_ID, Branch.Name as BRANCH_NAME
from Employee, branch
where employee.Assigned_Branch_ID = branch.branch_ID
and DEPT_ID = 1
and Branch.Name = 'Headquarters'
