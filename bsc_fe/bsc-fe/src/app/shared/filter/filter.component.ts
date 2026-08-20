import { Component, Directive, EventEmitter, Input, Output } from '@angular/core';
import { NgbModule } from '@ng-bootstrap/ng-bootstrap';

@Component({
  selector: 'app-filter',
  standalone: true,
  imports: [NgbModule],
  templateUrl: './filter.component.html',
  styleUrl: './filter.component.css',
})
export class FilterComponent {
  @Input() selectedItem: string[] = [];
  @Output() onApplyFilter = new EventEmitter<string[]>();
  @Input() filterOptions: string[] = ['Aasddas', 'aasdasdB', 'CCCCCCC'];
  @Input() title: string = 'Filter';
  
  onReset(){
    this.selectedItem = [];
  }
  onUpdateSelected(value: string) {
    if (this.selectedItem.includes(value)) {
      this.selectedItem = this.selectedItem.filter((t) => t != value);
    } else {
      this.selectedItem.push(value);
    }
  }
  applyFilter() {
    this.onApplyFilter.emit(this.selectedItem);
  }
}
