import { Component, OnInit, ChangeDetectorRef } from '@angular/core';
import { CommonModule } from '@angular/common';
import { FormsModule } from '@angular/forms';
import { Router } from '@angular/router';
import { ProductService, Product } from '../../services/product.service';
import { CategoryService, Category } from '../../services/category.service';

@Component({
  selector: 'app-product-list',
  standalone: true,
  imports: [CommonModule, FormsModule],
  templateUrl: './product-list.html',
  styleUrls: ['./product-list.css'],
})
export class ProductListComponent implements OnInit {
  products: Product[] = [];
  categories: Category[] = [];

  searchTerm: string = '';
  selectedCategory: number | undefined = undefined;

  currentPage: number = 1;
  pageSize: number = 10;
  totalCount: number = 0;
  totalPages: number = 1;

  // Set to true by default to freeze the DOM table on initial mount
  isLoading: boolean = true;

  constructor(
    private productService: ProductService,
    private categoryService: CategoryService,
    private router: Router,
    private cdr: ChangeDetectorRef
  ) { }

  // Triggers automatically when coming back to the page if component recreates
  ngOnInit(): void {
    this.loadCategories();
    this.refreshTableData();
  }

  // Angular router hook that triggers every single time this view becomes active
  // This bypasses component caching entirely when returning from another page
  ionViewWillEnter(): void {
    this.refreshTableData();
  }

  loadCategories(): void {
    this.categoryService.getCategories().subscribe({
      next: (data) => {
        this.categories = data || [];
        this.cdr.markForCheck();
      },
      error: (err) => console.error('Error loading categories', err)
    });
  }

  // The single, centralized point of execution for every UI event
  refreshTableData(): void {
    // 1. Instantly freeze the template DOM
    this.isLoading = true;
    this.cdr.detectChanges();

    const searchString = this.searchTerm ? this.searchTerm.trim() : '';

    this.productService.getProducts(
      Number(this.currentPage),
      Number(this.pageSize),
      searchString || undefined,
      this.selectedCategory
    ).subscribe({
      next: (response) => {
        // Safe mapping supporting camelCase and PascalCase payloads
        const items = response?.items || (response as any)?.Items;
        const total = response?.totalCount !== undefined ? response.totalCount : (response as any)?.TotalCount;

        if (items && Array.isArray(items)) {
          this.products = items;
          this.totalCount = Number(total);
          this.totalPages = Math.ceil(this.totalCount / Number(this.pageSize)) || 1;
        } else {
          this.products = [];
          this.totalCount = 0;
          this.totalPages = 1;
        }

        // 2. Unfreeze the DOM layout ONLY after variables are completely bound
        this.isLoading = false;
        this.cdr.detectChanges();
      },
      error: (err) => {
        console.error('Data pull aborted by server request rejection:', err);
        this.products = [];
        this.isLoading = false;
        this.cdr.detectChanges();
      }
    });
  }

  onSearchOrFilter(): void {
    this.currentPage = 1;
    this.refreshTableData();
  }

  onPrevPage(): void {
    if (this.currentPage > 1) {
      this.currentPage = this.currentPage - 1;
      this.refreshTableData();
    }
  }

  onNextPage(): void {
    if (this.currentPage < this.totalPages) {
      this.currentPage = this.currentPage + 1;
      this.refreshTableData();
    }
  }

  onFirstPage(): void {
    this.currentPage = 1;
    this.refreshTableData();
  }

  onLastPage(): void {
    this.currentPage = this.totalPages;
    this.refreshTableData();
  }

  onPageSizeChange(): void {
    this.currentPage = 1;
    this.refreshTableData();
  }

  onEdit(product: Product): void {
    this.router.navigate(['/product-form', product.id]);
  }

  onDelete(product: Product): void {
    if (confirm(`Are you sure you want to delete "${product.name}"?`)) {
      this.productService.deleteProduct(product.id).subscribe({
        next: () => {
          alert('Product deleted successfully.');
          if (this.products.length === 1 && this.currentPage > 1) {
            this.currentPage--;
          }
          this.refreshTableData();
        },
        error: (err) => alert('Could not delete product.')
      });
    }
  }
}
