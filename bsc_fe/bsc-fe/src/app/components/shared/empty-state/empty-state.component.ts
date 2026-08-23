import { Component } from '@angular/core';
import { AnimationItem } from 'lottie-web';
import { AnimationOptions, LottieComponent } from 'ngx-lottie';

@Component({
  selector: 'app-empty-state',
  standalone: true,
  imports: [LottieComponent],
  templateUrl: './empty-state.component.html',
  styleUrl: './empty-state.component.css'
})
export class EmptyStateComponent {
  options: AnimationOptions = {
    path: 'assets/empty_data.json',
    loop: true,
    autoplay: true
  }
  
  animationItem: AnimationItem | undefined; 
  animationCreated(animationItem: AnimationItem): void {
    this.animationItem = animationItem;
  }
}
