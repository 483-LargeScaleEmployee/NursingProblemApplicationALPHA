# Data Format
|Type|  # of Options| Space Taken|
|--:|:--:|:--:|
| DEPARTMENT | 10 |4 bits|
|ROLE|3|4 bits
|USERID|64^2|2 bytes
|SCHEDULE|3^14|6 bytes
### Example:
	RAW: 000100100011010101110010001001001001001001001001001001001001001001 
	SEPARATED BY TYPE:
	0011 0010 0001010101110010 001001001001001001001001001001001001001001
	EXPLAINED:
	0011 			:= 3 		:= 3rd Department
	0010 			:= 2 		:= 2nd Role
	0001010101110010	:= 5490		:= Employee #5490
	001001...		:= Shift 3	:= Works Shift 3 every day

# Getters
	get_Employee(USERID) 				:= {DEPARTMENT, ROLE, SCHEDULE}
	get_Employee(USERID, USERID, ...)		:= {{DEPARTMENT, ROLE, SCHEDULE},...}
	get_Employee_Schedule(USERID)			:= {SCHEDULE}
	get_Employee_Schedule(USERID, USERID, ...)	:= {SCHEDULE, SCHEDULE, ...}
	get_Department_List(DEPARTMENT)			:= {USERID, USERID, ...}
	get_Department_Role(DEPARTMENT, ROLE)		:= {USERID, USERID, ...}

# Notes
1. DEPARTMENT 0000 is a BLANK department.
2. DEPARTMENT goes from 0001-1010 (1-10)
3. SHIFT breakdown:
	* 1XX	: Works shift 1
	* X1X	: Works shift 2
	* XX1	: Works shift 3
	* 000	: No shifts
