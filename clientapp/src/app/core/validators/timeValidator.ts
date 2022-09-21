import { AbstractControl, ValidationErrors, ValidatorFn } from "@angular/forms";
import { createDateFromTimeString } from "@app/shared/helpers/timeHelpers";

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

