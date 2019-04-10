select EMP_ID, FIRST_NAME, LAST_NAME, START_DATE, TITLE, DEPT_ID
from EMPLOYEE
where year(START_DATE) in (2000,2001,2002)
and DEPT_ID = 1
order by TITLE asc, Start_Date desc
