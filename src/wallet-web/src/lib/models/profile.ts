import { z } from "zod";

export type Profile = {
    id: string;
    name: string;
    description: string;
    userId: string;
    currency: string;
    createdAt: Date;
};

export const createProfileSchema = z.object({
    name: z.string().min(1),
    description: z.string().min(1),
    currency: z.string().min(1),
});

export type CreateProfileSchema = typeof createProfileSchema;
export type CreateProfileRequest = z.infer<CreateProfileSchema> & { userId: string };