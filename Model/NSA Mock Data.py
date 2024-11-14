import csv
import random

# Constants
NUM_EMPLOYEES = 100
DEPARTMENTS = [
    "Emergency", "Cardiology", "Neurology", "Intensive Care Unit", 
    "Pediatrics", "Gynecology", "Radiology", "Psychiatry", "Orthopedics", "Oncology"
]
ROLES = ["Admin", "Doctor", "Nurse"]
DAYS = list(range(1, 15))  # Days 1 through 14
SHIFTS = ["001", "010", "011", "100", "101", "110"]  # Valid shifts without three consecutive "1"s

# Helper function to generate random schedule
def generate_random_schedule():
    # Randomly select 3 to 5 unique days for shifts
    chosen_days = sorted(random.sample(DAYS, k=random.randint(3, 5)))
    # Assign a valid shift code for each chosen day
    shift_pattern = [random.choice(SHIFTS) for _ in chosen_days]
    return chosen_days, shift_pattern

# Write data to CSV
with open("employee_schedule.csv", mode="w", newline="") as file:
    writer = csv.writer(file)
    # Write headers
    writer.writerow(["EID", "Department", "Role", "Day", "Shift"])

    # Generate entries
    for eid in range(1, NUM_EMPLOYEES + 1):
        department = random.choice(DEPARTMENTS)
        role = random.choice(ROLES)
        days, shifts = generate_random_schedule()
        
        # Format days and shifts as required
        days_str = f"({', '.join(map(str, days))})"
        shifts_str = f"({', '.join(shifts)})"
        
        # Write the row for each employee
        writer.writerow([eid, department, role, days_str, shifts_str])

print("CSV file 'employee_schedule.csv' created successfully.")
