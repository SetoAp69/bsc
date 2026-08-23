import {
  Component,
  EventEmitter,
  inject,
  Input,
  OnInit,
  Output,
} from '@angular/core';
import { GigDetail } from '../../../interfaces/gig.interface';
import { FormBuilder, FormGroup, ReactiveFormsModule } from '@angular/forms';
import {
  CreateEditGigForm,
  formToRequest,
} from '../../../interfaces/gig-create-edit-form';
import {
  GigRequestArrayValidator,
  GigRequestNumberValidator,
  GigRequestStringValidator,
} from '../../../validators/gig-request-validators';
import { GigService } from '../../../services/gig.service';
import { ActivatedRoute, Router } from '@angular/router';
import { NgbToast } from '@ng-bootstrap/ng-bootstrap';
import { TypeService } from '../../../services/type.service';
import { Type } from '../../../interfaces/type';
import { GigTypeSelectorComponent } from '../../gig-create/gig-type-selector/gig-type-selector.component';
import { LoadingComponent } from '../../shared/loading/loading.component';
import { CommonModule } from '@angular/common';

@Component({
  selector: 'app-gig-edit',
  standalone: true,
  imports: [
    NgbToast,
    ReactiveFormsModule,
    GigTypeSelectorComponent,
    LoadingComponent,
    CommonModule,
  ],
  templateUrl: './gig-edit.component.html',
  styleUrl: './gig-edit.component.css',
})
export class GigEditComponent implements OnInit {
  private gigService = inject(GigService);
  private typeService = inject(TypeService);
  private router = inject(Router);
  private route = inject(ActivatedRoute);
  private fb = inject(FormBuilder);
  ngOnInit(): void {
    this.fetchTypes();
    this.fetchDetail();
  }
  private id = +(this.route.snapshot.parent?.paramMap.get('gigId') ?? '');
  types: Type[] = [];
  isLoading: boolean = false;
  detail: GigDetail = {
    id: 0,
    name: '',
    description: '',
    duration: 0,
    price: 0,
    stars: 0,
    gigCreator: {
      id: 0,
      name: '',
    },
    types: [],
  };
  toastState: boolean | null = null;
  toastMessage: string = '';
  createGigForm: FormGroup<CreateEditGigForm> = this.fb.group({
    name: [
      this.detail.name,
      [GigRequestStringValidator(true, 100, "Gig's name")],
    ],
    description: [
      this.detail.description,
      [GigRequestStringValidator(false, 100, "Gig's name")],
    ],
    duration: [
      this.detail.duration,
      [GigRequestNumberValidator(1, null, 'Duration')],
    ],
    price: [this.detail.price, GigRequestNumberValidator(1, null, 'Price')],
    types: [
      this.detail.types.map((type) => type.id),
      [GigRequestArrayValidator(true, null, "Gig's type")],
    ],
  });

  OnSubmit() {
    this.isLoading = true;
    const reqBody = formToRequest(this.createGigForm);
    this.gigService.updateGig(this.detail.id, reqBody).subscribe({
      next: (res) => {
        this.onShowToast(true, 'Gig updated successfully');
        this.router.navigate(['../'], { relativeTo: this.route }).then(() => {
          window.location.reload();
        });
        this.isLoading = false;
      },
      error: (err) => {
        console.error('Error updating gig', err);
        this.onShowToast(false, 'Error updating gig');
        this.isLoading = false;
      },
    });
  }
  onShowToast(state: boolean, message: string) {
    this.toastState = state;
    this.toastMessage = message;
  }
  fetchTypes() {
    this.typeService.getTypes().subscribe({
      next: (res) => (this.types = res),
    });
  }
  fetchDetail() {
    this.gigService.getGigById(this.id).subscribe({
      next: (res) => {
        this.detail = res;
        this.formFromDetail(this.detail);
      },
    });
  }
  formFromDetail(detail: GigDetail) {
    this.createGigForm.patchValue({
      name: detail.name,
      description: detail.description,
      duration: detail.duration,
      price: detail.price,
      types: detail.types.map((type) => type.id),
    });
  }
}
