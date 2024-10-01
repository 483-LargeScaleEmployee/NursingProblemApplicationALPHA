# Data Format
## Employee Information
### Getters
#### Fetch USERID
	get_Department_List(DEPARTMENT)			:= {USERID, ...}
	get_Department_Role(DEPARTMENT, ROLE)	:= {USERID, ...}
	
#### Fetch Employee Info
	get_Employee(USERID) 					:= {DEPARTMENT, ROLE, AVAILABILITY, PREFERRED}
	get_Employee(USERID, ...)				:= {{DEPARTMENT, ROLE, AVAILABILITY, PREFERRED}, ...}
	get_Employee_Schedule(USERID)			:= {AVAILABILITY, PREFERRED}
	get_Employee_Schedule(USERID, ...)		:= {{AVAILABILITY, PREFERRED}, ...}
	
	get_Employee_Availability(USERID)		:= {AVAILABILITY, PREFERRED}
	get_Employee_Availability(USERID, ...)	:= {AVAILABILITY, ...}
	
	get_Employee_Preferred(USERID)			:= {PREFERRED}
	get_Employee_Preferred(USERID, ...)		:= {PREFERRED, ...}
### Data Info

|Type|  # of Options| Space Taken|
|--:|:--:|:--:|
| DEPARTMENT | 10 |4 bits|
|ROLE|3|4 bits
|USERID|2^16|2 bytes
|AVAILABILITY|4^14|7 bytes
|PREFERRED|4^14|7 bytes
#### Example:
	RAW: 000100100011010101110010-
		-00110011001100110011001100110011001100110011001100110011-
		-00010001000100010001000100010001000100010001000100010001 
	
	HEX: 3215723333333333333311111111111111
	
	SEPARATED BY TYPE:
	0011 0010 0001010101110010
	00110011001100110011001100110011001100110011001100110011
	00010001000100010001000100010001000100010001000100010001
	
	SEPERATED BY TYPE (HEX):
	3 2 1572 33333333333333 11111111111111
EXPLAINED:
|Type|  Binary| Hex|Decimal| Meaning|
|--:|:--:|:--:|:--:|:--:|
DEPARTMENT|0011 	| 3 |	3	| 3rd Department
ROLE|0010 	|2 	|2| 2nd Role
USERID|0001010101110010	|1572 |5490	|	 Employee #5490
AVAILABILITY|00110011...|33...| 33...| Available Shifts 2 & 3 daily
PREFERRED|00010001...|11...| 11...| Prefers Shift 3 daily




### Lookup Table for AVAILABILITY
Binary| Hex|Decimal| Meaning|
:--:|:--:|:--:|:--|
0000|0|0|Unavailable
0001|1|1|Available Shift 3
0010|2|2|Available Shift 2
0011|3|3|Available Shifts 2 & 3
0100|4|4|Available Shift 1
0101|5|5|Available Shifts 1 & 3
0110|6|6|Available Shifts 1 & 2
0111|7|7|Available Shifts 1 & 2 & 3
1XXX|8-F|8-15|Sick, Unavailable
### Lookup Table for PREFERRED
Binary| Hex|Decimal| Meaning|
:--:|:--:|:--:|:--|
0000|0|0|Unavailable
0001|1|1|Prefers Shift 3
0010|2|2|Prefers Shift 2
0011|3|3|Prefers Shifts 2 & 3
0100|4|4|Prefers Shift 1
0101|5|5|Prefers Shifts 1 & 3
0110|6|6|Prefers Shifts 1 & 2
0111|7|7|Prefers Shifts 1 & 2 & 3
1000|8|8|PTO, Unavailable
1001|9|9|PTO, Prefers Shift 3
1010|A|10|PTO, Prefers Shift 2
1011|B|11|PTO, Prefers Shifts 2 & 3
1100|C|12|PTO, Prefers Shift 1
1101|D|13|PTO, Prefers Shifts 1 & 3
1110|E|14|PTO, Prefers Shifts 1 & 2
1111|F|15|PTO, Prefers Shift 1 & 2 & 3


### Notes
1. DEPARTMENT 0000 is a BLANK department.
2. DEPARTMENT goes from 0001-1010 (1-10).
3. AVAILABILITY breakdown:
	* X1XX	: Available shift 1
	* XX1X	: Available shift 2
	* XXX1	: Available shift 3
	* X000	: No shifts
	* 0XXX	: Regular Day
	* 1XXX	: Sick Day
4. PREFERRED breakdown:
	* X1XX	: Prefers shift 1
	* XX1X	: Prefers shift 2
	* XXX1	: Prefers shift 3
	* X000	: No shifts
	* 0XXX	: Regular Day
	* 1XXX	: PTO Request
5. 1.06MB MAX per week (27.6MB MAX per year).
6. Hex representations listed for redundancy and for debugging purposes. Remember, its the exact same binary data, just an easier way to look at it for some.


## Shift Needs
### Getters
#### Fetch USERID
	get_Needs_Department(DEPARTMENT)					:= int
	get_Needs_Department_List(DEPARTMENT)				:= {int, ...}
	
	get_Needs_DepartmentByRole(DEPARTMENT, ROLE)		:= int
	get_Needs_DepartmentByRole_List(DEPARTMENT, ROLE)	:= {int, ...}

### Data Info
|Type|  # of Options| Space Taken|
|--:|:--:|:--:|
DEPARTMENT|10|4 bits
ROLE|3|4 bits
REQUIRED|N/A|14 bytes
#### Example:
	RAW: 0011000101110111011101110111011101110111011101110111011101110111
	HEX: 3177777777777777
	SEPARATED BY TYPE: 	0011 0001 
						01110111011101110111011101110111011101110111011101110111
	SEPARATED BY TYPE (HEX): 3 1 77777777777777
EXPLAINED:
|Type|  Binary| Hex|Decimal| Meaning|
|--:|:--:|:--:|:--:|:--:|
DEPARTMENT|0011|3|3| 3rd Department
ROLE|0001|1|1|	1st Role
REQUIRED|01110111...|77...|77...|7 required daily for this Role for this Department
### Notes
1. REQUIRED is just a list of numbers between 0-255, 14 times.
2. 450 bytes per week (11.4KB per year).
