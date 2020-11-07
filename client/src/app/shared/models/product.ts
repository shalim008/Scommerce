export interface IProduct {
    id: number;
    name: string;
    description: string;
    price: number;
    pictureUrl: string;
    pictureFile: File;
    productType: string;
    productBrand: string;
    productImages: IProductImages[];
    gallaryImages: File[];
    productAttribute: IProductAttribute[];
    productAttributeValues: IProductAttributeValues[];
}

export interface IProductVariations {
    id: number;
    name: string;
    description: string;
    price: number;
    pictureUrl: string;
    varPictureUrl: File;
    regularPrice: string;
    stockQuantity: string;
    productWeight: string;
    productLength: string;
    productWidth: string;
    productHeight: string;
    productSKU: string;
    productAttributeId: string;
    productAttributeValueId: string;

}

export interface IProductImages {
    id: number;
    nameAlt: string;
    imageUrl: string;
    imageThumbUrl: number;
    imageBigUrl: string;
    isRelativeUrl: string;
}

export interface IProductAttribute {
    id: number;
    attributeName: string;
    isVisibleOnProductPage: string;
    isUsedForVariations: number;
}

export interface IProductAttributeValues {
    id: number;
    attributeValueName: string;
    attributeValueParentId: string;
    productAttributeId: number;
}

export interface IProductAttrValueMap{
    id: number;
    attributeName: string;
    productAttributeValues: IProductAttributeValues[];
}

