import { AbstractControl, ValidationErrors, ValidatorFn } from '@angular/forms';

export function GigRequestStringValidator(
  isRequired: boolean = true,
  maxLength: number | null,
  name: string = 'Data',
): ValidatorFn {
  return (control: AbstractControl): ValidationErrors | null => {
    let message: string | null = null;
    const value = control.value as string;
    if (isRequired && (value?.length??0) < 1) {
      message = `${name} can't be empty`;
    }

    if (maxLength != null && (value?.length??0) > maxLength) {
      message = `${name} can't be more than 100 char's length`;
    }

    return message == null ? null : { error: true, message: message };
  };
}

export function GigRequestArrayValidator(
  isRequired: boolean = true,
  maxLength: number | null,
  name: string = 'Data',
): ValidatorFn {
  return (control: AbstractControl): ValidationErrors | null => {
    let message: string | null = null;
    const value = control.value as number[];
    if (isRequired && (value?.length??0) < 1) {
      message = `${name} can't be empty`;
    }

    if (maxLength != null && (value?.length??0) > maxLength) {
      message = `${name} can't be more than 100`;
    }

    return message == null ? null : { error: true, message: message };
  };
}

export function GigRequestNumberValidator(
  minValue: number | null = null,
  maxValue: number | null = null,
  name: string = 'Data',
): ValidatorFn {
  return (control: AbstractControl): ValidationErrors | null => {
    let message: string | null = null;
    const value = control.value as number;
    if (minValue != null && value < minValue) {
      message = `${name} can't be less than ${minValue}`;
    }
    if (maxValue != null && value > maxValue) {
      message = `${name} can't be more than ${maxValue}`;
    }

    return message == null ? null : { error: true, message: message };
  };
}
