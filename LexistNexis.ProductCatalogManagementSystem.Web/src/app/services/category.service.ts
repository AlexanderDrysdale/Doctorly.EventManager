import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Observable } from 'rxjs';

export interface Category {
  id: number;
  name: string;
  description: string;
  parentCategoryId?: number | null;
}

// Matches CategoryNodeDto (tree)
export interface CategoryNode {
  id: number;
  name: string;
  description: string;
  children: CategoryNode[];
}

@Injectable({ providedIn: 'root' })
export class CategoryService {
  private apiUrl = 'https://localhost:7044/api/categories';

  constructor(private http: HttpClient) {}

  // GET all categories
  getCategories(): Observable<Category[]> {
    return this.http.get<Category[]>(this.apiUrl);
  }

  // POST (create) a new category
  addCategory(category: Category): Observable<Category> {
    return this.http.post<Category>(this.apiUrl, category);
  }

  // GET category tree
  getCategoryTree(): Observable<CategoryNode[]> {
    return this.http.get<CategoryNode[]>(`${this.apiUrl}/tree`);
  }
}
