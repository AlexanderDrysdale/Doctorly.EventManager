import { Component } from '@angular/core';

@Component({
  selector: 'app-category-management',
  imports: [],
  templateUrl: './category-management.html',
  styleUrl: './category-management.css',
})
export class CategoryManagement {
  categories = ['Electronics', 'Books', 'Clothing'];
  newCategory = '';

  addCategory() {
    if (this.newCategory && !this.categories.includes(this.newCategory)) {
      this.categories.push(this.newCategory);
      this.newCategory = '';
    }
  }

  removeCategory(cat: string) {
    this.categories = this.categories.filter(c => c !== cat);
  }
}
