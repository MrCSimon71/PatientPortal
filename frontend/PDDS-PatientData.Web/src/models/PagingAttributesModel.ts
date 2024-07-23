export interface LinksModel {
  self: string;
  first: string;
  prev: string | null;
  next: string | null;
  last: string;
};

export interface PagingAttributesModel {
  currentPage: number;
  totalPages: number;
  pageSize: number;
  pageNumber: number;
  totalRecords: number;
  links: LinksModel;
};

