import { ChangeDetectorRef, Component } from '@angular/core';
import { ProductService, Product } from '../../services/product.service';
import { ActivatedRoute, Router } from '@angular/router';
import { CommonModule } from '@angular/common';
import { FormsModule } from '@angular/forms';
import { MatFormFieldModule } from '@angular/material/form-field';
import { MatInputModule } from '@angular/material/input';
import { MatSelectModule } from '@angular/material/select';
import { MatButtonModule } from '@angular/material/button';
import { CategoryService, Category } from '../../services/category.service';
import { catchError, map, Observable, of } from 'rxjs';

@Component({
  selector: 'app-product-form',
  imports: [CommonModule,
    FormsModule,
    MatFormFieldModule,
    MatInputModule,
    MatSelectModule,
    MatButtonModule
  ],
  templateUrl: './product-form.html',
  styleUrl: './product-form.css',
  standalone: true
})
export class ProductFormComponent {
  categoriesList: Category[] = [];
  product: Product = {
    id: 0,
    name: '',
    description: '',
    sku: '',
    price: 0,
    quantity: 0,
    categoryId: 0,
    createdAt: new Date(),
    updatedAt: new Date()
  };

  constructor(
    private productService: ProductService,
    private categoryService: CategoryService,
    private route: ActivatedRoute,
    private cdr: ChangeDetectorRef,
    private router: Router
  ) { }

  ngOnInit(): void {
    const id = this.route.snapshot.paramMap.get('id');
    if (id) {
      this.productService.getProduct(+id).subscribe(p => {
        this.product = p;
        this.cdr.detectChanges();
      });
      this.categoryService.getCategories().subscribe(c => {
        this.categoriesList = c;
        this.cdr.detectChanges();
      });
    }
  }

  saveProduct() {
    if (this.product.id && this.product.id > 0) {
      this.productService.updateProduct(this.product.id, this.product)
        .subscribe(updated => console.log('Updated:', updated));
    } else {
      this.productService.addProduct(this.product)
        .subscribe(created => console.log('Created:', created));
    }
  }

  cancel() {
    this.router.navigate(['/']); // Adjust to your real inventory route path hook
  }
}
