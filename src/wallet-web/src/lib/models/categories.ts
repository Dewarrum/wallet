import { z } from "zod";

export type Category = {
    id: string;
    userId: string;
    name: string;
    description: string;
    createdAt: Date;
}

export const createCategorySchema = z.object({
    name: z.string().min(1),
    description: z.string().min(1),
});

export type CreateCategorySchema = typeof createCategorySchema;
export type CreateCategoryRequest = z.infer<CreateCategorySchema> & { userId: string };