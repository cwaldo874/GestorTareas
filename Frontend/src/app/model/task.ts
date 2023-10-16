import { Category } from "./category";

export class Task {

  public id: number;
  public nombre: string;
  public descripcion: string;
  public fechaTarea: Date;
  public completada: boolean;
  public categoriaId: string;
  public categoria: Category;

}