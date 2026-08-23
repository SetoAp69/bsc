import { Component, EventEmitter, Input, Output, forwardRef } from '@angular/core';
import { Type } from '../../../interfaces/type';
import { ControlValueAccessor, NG_VALUE_ACCESSOR } from '@angular/forms';

@Component({
  selector: 'app-gig-type-selector',
  standalone: true,
  imports: [],
  templateUrl: './gig-type-selector.component.html',
  providers: [
    {
      provide: NG_VALUE_ACCESSOR,
      useExisting: forwardRef(() => GigTypeSelectorComponent),
      multi: true
    }
  ],
  styleUrl: './gig-type-selector.component.css',
})
export class GigTypeSelectorComponent implements ControlValueAccessor {
  @Input() options: Type[] = [];

  selectedIds: number[] = [];

  onChange: any = () => {};
  onTouched: any = () => {};
  isDisabled = false;

  writeValue(value: number[]): void {
    if (value) {
      this.selectedIds = value;
    } else {
      this.selectedIds = [];
    }
  }
  registerOnChange(fn: any): void {
    this.onChange = fn
  }
  registerOnTouched(fn: any): void {
    this.onTouched = fn
  }
  setDisabledState?(isDisabled: boolean): void {
    this.isDisabled = isDisabled;
  }

  toggleType(type: Type) {
    if (this.isDisabled) return;
    if (this.isSelected(type.id)) {
      this.selectedIds = this.selectedIds.filter((id) => id !== type.id);
    } else {
      this.selectedIds = [...this.selectedIds, type.id];
      this.onChange(this.selectedIds);
      this.onTouched();
    }
  }
  isSelected(id: number): Boolean {
    return this.selectedIds.includes(id);
  }
  getTypeName(id: number): string {
    return this.options.find((type) => type.id == id)?.name ?? '';
  }
}
