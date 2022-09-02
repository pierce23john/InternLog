import { AbstractControl, ValidationErrors, ValidatorFn } from "@angular/forms";

export function timeValidator(startName, endName): ValidatorFn {
  return (control: AbstractControl): ValidationErrors | null => {
    const start = control.get(startName);
    const end = control.get(endName);

    const startDate = createDateFromTimeString(start?.value);
    const endDate = createDateFromTimeString(end?.value);

    return start && end && startDate > endDate
      ? { invalidTime: "Start time must not be earlier than End time" }
      : null;
  };
}

const createDateFromTimeString = (time) => {
  const hour = Number(time.split(":")[0]);
  const minute = Number(time.split(":")[1]);

  const date = new Date();
  date.setHours(hour);
  date.setMinutes(minute);

  return date;
};
